using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public int lineNo { get; set; }

        public int quantity { get; set; }

        public double costEach { get; set; }

        public double costSum { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemPrice { get; set; }

        public Item()
        {

        }

    }
}
