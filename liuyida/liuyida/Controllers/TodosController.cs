using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using liuyida.Models;

namespace liuyida.Controllers
{
    [Authorize]
    public class TodosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Todos
        public ActionResult Index()
        {

            var ordersQuery = db.Orders.Where(o => DbFunctions.TruncateTime(o.DeliveryTime) == DbFunctions.TruncateTime(DateTime.Today) 
                    || DbFunctions.TruncateTime(o.DeliveryTime) == DbFunctions.AddDays(DateTime.Today,1)).Include(o => o.Customer).OrderBy(o => o.DeliveryTime);
            IDictionary<DateTime, IList<Order>> orderByTime = new Dictionary<DateTime, IList<Order>>();
            IDictionary<DateTime, IDictionary<Product,int>> orderItemByTime = new Dictionary<DateTime, IDictionary<Product, int>>();

            IList<DateTime> dateTimes = new List<DateTime>();
            dateTimes.Add(DateTime.Today.AddHours(10));
            dateTimes.Add(DateTime.Today.AddHours(14));
            dateTimes.Add(DateTime.Today.AddHours(20));
            dateTimes.Add(DateTime.Today.AddHours(34));
            dateTimes.Add(DateTime.Today.AddHours(38));
            dateTimes.Add(DateTime.Today.AddHours(44));

            int i = 0;
            foreach (var t in dateTimes)
            {
                orderByTime.Add(t, new List<Order>());
                orderItemByTime.Add(t, new Dictionary<Product, int>());
            }
            var orders = ordersQuery.ToList();
            foreach (var o in orders)
            {
                if (i == dateTimes.Count() - 1)
                {
                    orderByTime[dateTimes[i]].Add(o);
                    foreach (var oi in o.OrderItems)
                    {
                        if (orderItemByTime[dateTimes[i]].ContainsKey(oi.Product))
                        {
                            orderItemByTime[dateTimes[i]][oi.Product] += oi.Quantity;
                        }
                        else
                        {
                            orderItemByTime[dateTimes[i]].Add(oi.Product, oi.Quantity);
                        }
                    }
                }
                else if (o.DeliveryTime >= dateTimes[i] && o.DeliveryTime < dateTimes[i + 1])
                {
                    orderByTime[dateTimes[i]].Add(o);
                    foreach (var oi in o.OrderItems)
                    {
                        if (orderItemByTime[dateTimes[i]].ContainsKey(oi.Product))
                        {
                            orderItemByTime[dateTimes[i]][oi.Product] += oi.Quantity;
                        }
                        else
                        {
                            orderItemByTime[dateTimes[i]].Add(oi.Product, oi.Quantity);
                        }
                    }
                }
                else if (o.DeliveryTime >= dateTimes[i + 1])
                {
                    do
                    {
                        i++;
                    }
                    while (o.DeliveryTime >= dateTimes[i + 1] && (i != dateTimes.Count() - 1));
                    orderByTime[dateTimes[i]].Add(o);
                    foreach (var oi in o.OrderItems)
                    {
                        if (orderItemByTime[dateTimes[i]].ContainsKey(oi.Product))
                        {
                            orderItemByTime[dateTimes[i]][oi.Product] += oi.Quantity;
                        }
                        else
                        {
                            orderItemByTime[dateTimes[i]].Add(oi.Product, oi.Quantity);
                        }
                    }
                }
            }
            ViewBag.OrderByTime = orderByTime;
            ViewBag.OrderItemByTime = orderItemByTime;
            return View();
        }
    }
}