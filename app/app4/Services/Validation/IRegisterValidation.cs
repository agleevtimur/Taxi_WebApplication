using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app4.Services
{//поля формы регистрации подвергаются валидации (кроме пароля)
    interface IRegisterValidation
    {
        public PatternClass Login => new PatternClass()
        {
            Mistake = 
            {
                { @".[!,?].*", "Имя пользователя не должно включать специальные символы !,?" },
                    { @"^[0-9]", "Имя пользователя не должно начинаться с цифры"},
                    { @"(.)\1{4,}", "Имя пользователя не должно включать повторяющиеся подряд символы" }
            },
            Pattern = 
            {
                //{ @"^[a-zA-Z]+\w+$", "Не корректное имя пользователя" }
            }
        };
        public PatternClass Email => new PatternClass()
        {
            Mistake =
                {
                     { @"@spam.com$", "Данный домен находится в спам - базе. Выберите другой почтовый сервис"}
                },
            Pattern =
                {
                     { @".*@.*", "Email должен содержать \'@\'" },
                     { @"^\w+@[a-z]+\.[a-z]+(\.[a-z]+)?$", "Некорректный email" }
                }
        };
    }
}
