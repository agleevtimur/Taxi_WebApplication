using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.Algorithms
{
    public class Algorithm
    {
        private readonly ApplicationContext context;

        public Algorithm(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<int>> Find(string time, string start, string finish, int countPerson, string clientId)
        {
            var list = new List<int>();
            IRepository repository = new Repository(context);
            DeleteOldRequests();//удаляем все просроченные реквесты
            await repository.UpdateClient(clientId);//обновляем данные по клиенту: число поездок++;остаток по приоритету--;если становится 0, то приоритет меняется на базовый
            var client = await repository.GetClient(clientId);//получаем клиента по id
            var newRequest = Extension.ParseToOrder(time, start, finish, countPerson, client.Id, client.Priority);//собираем из данных реквест
            await repository.SaveRequest(newRequest);//добавляем в репозиторий новый заказ,получаем Id для заказа
            var comlete = AggregateComplete(newRequest);//вызываем чекер на набор такси, возвращает класс Complete
            if (!comlete.IsComplete)//если такси не собрано, возвращаем null
                return null;
            var reqs = comlete.enumReqs;//иначе возвращаем список собранных заказов
            reqs.ForEach(x => repository.DeleteRequest(x.Id));//и удаляем данные реквесты из таблицы, обновляем значения таблиц ReadyOrders(логика репозитория)
            var order = new ReadyOrders(reqs.First().StartPointId, reqs.First().FinishPointId,
                DateTime.Now, reqs.First().OrderTime);
            await repository.SaveOrder(order);
            list.Add(await repository.GetSaveOrderId(order));
            var clients = reqs.Select(x => repository.GetClientForOrders(x.UserId));//возвращаем список найденных клиентов
            foreach (var user in clients)
            {
                list.Add(user.Id);
                user.CountOfTrips++;
                await repository.EditClient(user);
            }
            return list;
        }

        private void DeleteOldRequests()
        {
            IRepository repository = new Repository(context);
            var allReqs = repository.GetRequests().ToList();
            allReqs.ForEach(x =>
            {
                if (x.DepartureTime < DateTime.Now)
                    repository.DeleteRequest(x.Id);
            });
        }

        //public void DeleteCanceledRequest(Client client)
        //{
        //    IRepository repository = new Repository(context);
        //    repository.SaveClient(client);
        //    var reqs = repository.GetRequestsByClientId(client.Id);
        //    foreach (var req in reqs)
        //        repository.DeleteRequest(req.Id);
        //}

        private Complete AggregateComplete(Order newestRequest)//здесь проходит агрегация заказов, попытка собрать группу попутчиков
        {
            IRepository repository = new Repository(context);
            var suitedOrders = repository.GetRequests()
                .Where(x => SuitOrders(x, newestRequest))//находим подходящие
                .OrderByDescending(x => x.Priority);//сначала ищутся с самым высоким приоритетом
            var count = newestRequest.CountOfPeople;
            var complete = Complete.TryComplete(count, suitedOrders, newestRequest);
            return complete;
        }

        private static bool SuitOrders(Order req1, Order req2)//предикат для поиска похожих заказов
        {
            return req1.StartPointId == req2.StartPointId
            && req1.FinishPointId == req2.FinishPointId
            && req1.DepartureTime == req2.DepartureTime
            && req1.UserId != req2.UserId;
        }
    }

    public static class Extension
    {
        public static Order ParseToOrder(string time, string start, string finish, int countPerson, int clientId, int priority)//возвращаем ордер из сообщения cmdFind
        {
            return new Order(int.Parse(start), int.Parse(finish), countPerson, AroundTime(time), DateTime.Now, priority, clientId);
        }

        private static DateTime AroundTime(string time)
        {
            var today = DateTime.Now;
            var split = time.Split(':', '.', ',', ';');//возможный парсинг строки со временем
            var hours = int.Parse(split[0]);
            var minutes = int.Parse(split[1]);
            var aroundMinutes = (minutes / 10) * 10;
            var date = new DateTime(today.Year, today.Month, GetDay(hours, minutes), hours, aroundMinutes, 0);
            return date;
        }

        private static int GetDay(int hours, int minutes)
        {
            var totalM = minutes + 60 * hours;
            var today = DateTime.Now;
            var hoursCurrent = today.Hour;
            var minutesCurrent = today.Minute;
            var totalMCurrent = minutesCurrent + 60 * hoursCurrent;

            if (totalM < totalMCurrent)
            {//заказ на следующий день
                return today.Day + 1;
            }
            return today.Day;//заказ на этот день
        }
    }

    public class Complete
    {
        public readonly bool IsComplete;
        public readonly List<Order> enumReqs;
        public static Complete Empty = new Complete(false, null);
        public Complete(bool isComplete, List<Order> completeOrders)
        {
            IsComplete = isComplete;
            enumReqs = completeOrders;
        }

        public static Complete Orders3(IEnumerable<Order> suitedOrders, Order newestOrder)//ищем первую единицу
        {
            var order1first = GetFirstRequest(suitedOrders, 1);
            if (order1first == null) return Empty;//ищем одну единицу
            return new Complete(true, new List<Order> { newestOrder, order1first });
        }

        public static Complete Orders2(IEnumerable<Order> suitedOrders, Order newestOrder)
        {
            var completeOrders = new List<Order> { newestOrder };
            var order2first = GetFirstRequest(suitedOrders, 2);
            if (order2first != null)//сначала к двум пытаемся найти еще два
            {
                completeOrders.Add(order2first);
                return new Complete(true, completeOrders);
            }//иначе ищем две единицы

            var order1count = suitedOrders.Where(x => x.CountOfPeople == 1);
            if (order1count.Count() >= 2)
            {
                completeOrders.AddRange(order1count.Take(2));
                return new Complete(true, completeOrders);
            }
            return Empty;//если двух единиц нет
        }

        private static Order GetFirstRequest(IEnumerable<Order> requests, int countOfPeople)
        {
            return requests.FirstOrDefault(x => x.CountOfPeople == countOfPeople);
        }

        public static Complete Orders1(IEnumerable<Order> suitedOrders, Order newestOrder)
        {
            var completeOrders = new List<Order> { newestOrder };
            var order3first = GetFirstRequest(suitedOrders, 3);
            if (order3first != null)//ищем одну тройку
            {
                completeOrders.Add(order3first);
                return new Complete(true, completeOrders);
            }
            var req2First = GetFirstRequest(suitedOrders, 2);
            if (req2First != null)//иначе ищем одну двойку и одну единицу
            {
                var req1First = GetFirstRequest(suitedOrders, 1);
                if (req1First != null)
                {
                    completeOrders.Add(req2First);
                    completeOrders.Add(req1First);
                    return new Complete(true, completeOrders);
                }
            }
            var req1Many = suitedOrders.Where(x => x.CountOfPeople == 1);
            if (req1Many.Count() >= 3)//ищем три и более единицы
            {
                completeOrders.AddRange(req1Many.Take(3));
                return new Complete(true, completeOrders);
            }
            return Empty;
        }

        public static Complete TryComplete(int count, IEnumerable<Order> suitedOrders, Order newestOrder)
        {
            switch (count)
            {
                case 1://ищем сначала 3, потом 2 1, потом 1 1 1
                    return Orders1(suitedOrders, newestOrder);
                case 2://ищем cначала 2, потом 1 1
                    return Orders2(suitedOrders, newestOrder);
                case 3://ищем сначала 1
                    return Orders3(suitedOrders, newestOrder);
                default:
                    return Empty;
            }
        }
    }
}
