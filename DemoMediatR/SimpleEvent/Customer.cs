using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.SimpleEvent
{
    public class Customer
    {
        // EventHandler
        //public event EventHandler OrderEvent;
        //public event EventHandler<Order> OrderEvent;
        // Delegate
        public delegate void OrderEventAction(Customer customer, Order order);
        public event OrderEventAction? OrderEvent;
        public double Bill { get; set; }
        private void SitDown()
        {
            Console.WriteLine("Customer: Sit down.");
        }
        private void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Customer: Let me think...");
                Thread.Sleep(1000);
            }
            OnOrder(new Order()
            {
                Item = DishItem.Toast,
                Size = DishSize.Small,
            });
        }
        private void Pay()
        {
            Console.WriteLine("Customer: I pay ${0}.", this.Bill);
        }
        protected void OnOrder(Order order)
        {
            OrderEvent?.Invoke(this, order);
        }
        public void Action()
        {
            SitDown();
            Think();
            Pay();
        }
    }
}
