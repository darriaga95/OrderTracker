using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName = "";
        public string CustomerPhone = "";
        public string CustomerEmail = "";

        public String Addressl1 = "";
        public String Addressl2 = "";
        public String City = "";
        public String State = "";
        public String Zip = "";

        public bool Success = false;

        public Customer() 
        {

        }

        public bool ParseAddress(String address)
        {
            
            if (address == null)
            {
                return Success;
            }

            this.Addressl1 = address.Substring(3, 50);

            this.Addressl2 = address.Substring(53, 50);

            this.City = address.Substring(103, 50);

            this.State = address.Substring(153, 2);

            this.Zip = address.Substring(155, 10);

            Success = true;

            return Success;
        }
    }
}
 