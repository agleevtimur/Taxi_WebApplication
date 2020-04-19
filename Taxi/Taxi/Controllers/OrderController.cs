using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Context;

namespace Taxi.Controllers
{
    public class OrderController : Controller
    {
        private readonly RatingContext ratingContext;
        private readonly ApplicationContext context;

        public OrderController(RatingContext ratingContext, ApplicationContext context)
        {
            this.ratingContext = ratingContext;
            this.context = context;
        }
        // готовые заказы, заказать, рейтинг, история заказов, все заказы

    }
}
