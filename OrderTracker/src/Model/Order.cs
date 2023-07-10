using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Model
{
    public class Order
    {
        public Customer OrderCustomer { get; set; }

        public List<Item> Items { get; set; }

        public String lineId { get; set; }
        public int orderNumber { get; set; }
        public int totalItems { get; set; }
        public Double totalCost { get; set; }
        public String orderDate { get; set; }
        public int isPaid { get; set; }
        public int isShipped { get; set; }
        public int isCompleted { get; set; }

        public bool Success;

        public String ErrorMsg;

        /**
         * - Order Object Model -
         */
        public Order()
        {
            OrderCustomer = new Customer();
            Items = new List<Item>();
            Success = false;
            ErrorMsg = string.Empty;
        }

        public int AddLineItem(Item item)
        {
            try
            {
                Items.Add(item);
            } catch 
            {
                return -1;
            }
            return 1;
        }

    

    }
}
