using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.SimpleEvent
{
    public class Order
    {
        public DishItem Item { get; set; }
        public DishSize Size { get; set; }
    }
}
