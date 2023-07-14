using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using OrderTracker.src.Controller;

namespace OrderTracker
{
    public class LaunchOrderTracker
    {

        public static void Main(string []args)
        {
            int retVal;
            OrderReader orderReader = new OrderReader();


            Console.WriteLine("Launching Order Tracker...");

            try
            {
                retVal = orderReader.ExecuteOrderConsole();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error: Excveption Encountered");
            }
            
        }

       



    }

}
