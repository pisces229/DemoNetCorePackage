using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.SimpleEvent
{
    public class Waiter
    {
        // EventHandler
        //public static void Action(object sender, EventArgs order)
        //public static void Action(object sender, Order order)
        // Delegate
        public static void Action(Customer customer, Order order)
        {
            Console.WriteLine("Waiter: I will serve you the dish - {0}", order.Item);
            var price = 10D;
            switch (order.Size)
            {
                case DishSize.Small:
                    price *= 0.5;
                    break;
                case DishSize.Large:
                    price *= 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }
}
