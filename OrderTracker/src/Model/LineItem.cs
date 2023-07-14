using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Model
{
    public class LineItem
    {
        public int lineNo = 0;

        public int quantity = 0;

        public double costEach = 0.00;

        public double costSum = 0.00;

        public string ItemDescription = "";

        public String ErrorMsg;

        public bool Success = false;

        public int NumErr;

        public LineItem()
        {
            NumErr = 0;
            ErrorMsg = "";
        }
        public bool ParseLineItem(String lineItem)
        {
            

            if (lineItem == null || lineItem == "")
            {
                return Success;
            }
            
            if(int.TryParse(lineItem.Substring(3,2), out this.lineNo) == false ) 
            {
                ErrorMsg += "Invalid input: LineItem Number - " + lineItem.Substring(3,2);
                NumErr++;
            }

            if(int.TryParse(lineItem.Substring(5, 5), out this.quantity) == false)
            {
                ErrorMsg += "Invalid input: Line Item - Quantity" + lineItem.Substring(5, 5);
                NumErr++;
            }

            if(double.TryParse(lineItem.Substring(10, 10), out this.costEach) == false) 
            {
                ErrorMsg += "Invalid input: Line Item - Individual Cost" + lineItem.Substring(10, 10);
                NumErr++;
            }
            if(double.TryParse(lineItem.Substring(20,10), out this.costSum) == false) 
            {
                ErrorMsg += "Invalid input: Line Item - Total Cost" + lineItem.Substring(20,10);
                NumErr++;
            }

            if((this.ItemDescription = lineItem.Substring(30,50)).Length == 0) 
            {
                ErrorMsg += "Invalid input: Line Item - Description - " + lineItem.Substring(30, 50);
                NumErr++;
            }

            if(ErrorMsg.Equals(""))
            {
                this.Success = true;
            }

            return Success;
        }
    }

   
}
