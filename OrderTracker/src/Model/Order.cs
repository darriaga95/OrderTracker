using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Model
{
    public class Order
    {
        public Customer OrderCustomer;
        public List<LineItem> LineItems;
        
        public int orderNumber = 0;
        public int totalItems = 0;
        public double totalCost = 0.00;
        public DateTime orderDate;
        public bool isPaid = false;
        public bool isShipped = false;
        public bool isCompleted = false;
        public bool Success = false;
        public String ErrorMsg;

        /**
         * - Order Object Model -
         */
        public Order()
        {
            OrderCustomer = new Customer();
            LineItems = new List<LineItem>();
            ErrorMsg = string.Empty;
            

        }
        public bool ParseOrder(String line)
        {
            int tempInt2Bool;
            bool retVal = true;

            if(int.TryParse(line.Substring(3, 10), out this.orderNumber) == false)
            {
                ErrorMsg += "Invalid input: Order Number - " + line.Substring(3,10);
                retVal = false;
            }

            if (int.TryParse(line.Substring(13, 5), out this.totalItems) == false)
            {
                ErrorMsg += "Invalid input: total items - " +  line.Substring(13,15);
                retVal = false;
            }

            if (double.TryParse(line.Substring(18, 10), out this.totalCost) == false)
            {
                ErrorMsg += "Invalid input: total cost - " + line.Substring(18,10);
                retVal = false;
            }

            if (DateTime.TryParse(line.Substring(28, 19), out this.orderDate) == false)
            {
                ErrorMsg += "Invalid input order date - " + line.Substring(28,19);
                retVal = false;
            }

            if((this.OrderCustomer.CustomerName = line.Substring(47, 50)).Length < 1)
            {
                ErrorMsg += "Invalid input Customer Name - " + line.Substring(47,50);
                retVal = false;
            }

            if ((this.OrderCustomer.CustomerPhone = line.Substring(97, 30)).Length < 1)
            {
                ErrorMsg += "Invalid input Customer Phone" + line.Substring(97,30);
                retVal = false;
            }

            if ((this.OrderCustomer.CustomerEmail = line.Substring(127, 50)).Length < 1)
            {
                ErrorMsg += "Invalid input Customer Email" + line.Substring(127,50);
                retVal = false;
            }

            if (int.TryParse(line.Substring(177, 1), out tempInt2Bool) == false)
            {
                ErrorMsg += "Invalid input: isPaid";
                retVal = false;
            }
            else
            {
                isPaid = (tempInt2Bool == 1) ? true : false;
            }

            if (int.TryParse(line.Substring(178, 1), out tempInt2Bool) == false)
            {
                ErrorMsg += "Invalid input: isShipped";
                retVal = false;
            }
            else
            {
                isShipped = (tempInt2Bool == 1) ? true : false;
            }

            if (int.TryParse(line.Substring(179, 1), out tempInt2Bool) == false)
            {
                ErrorMsg += "Invalid input: isCompleted";
                retVal = false;
            }
            else
            {
                isCompleted = (tempInt2Bool == 1) ? true : false;
            }

            Success = retVal;
            return retVal;
        }
            

    

    }
}
