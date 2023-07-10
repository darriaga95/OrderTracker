using OrderTracker.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Controller
{
    public class OrderReader /*Rename to order COnsole*/
    {
        

        // Path to input file
        private String OrderBufferPath;
        private Orders orders;


        public OrderReader(/*string orderPath*/
            ) 
        {
            OrderBufferPath = @"..\..\..\lib\OrderFile.txt";
            orders = new Orders();
        }

        public int ExecuteOrderConsole ()
        {
            String command = "";

            Console.WriteLine("Launching Console...");

            Console.WriteLine("Please enter a command -- run 'ot -help' for more information ");

            while (!command.Equals("quit"))
            {
                Console.Write("OT:>");

                try
                {
                    command = Console.ReadLine();

                } catch ( Exception ex ) 
                {
                    Console.WriteLine("Invalid Command - Please Try Again");
                }
                    
                  
                switch (command) 
                {
                    case "ot -start":
                        orders.ParseOrderFile(OrderBufferPath);
                        break;
                    case "ot -help":
                        DisplayHelp();
                        break;
                    default:
                        break;
                }
            }

            return 1;
        }

        public void DisplayHelp()
        {
            Console.WriteLine("\n-----Help Context Menu-----");
            Console.WriteLine("command                 description");
            Console.WriteLine("ot -start               launch OrderTracker with sample data");
            Console.WriteLine("ot -help                display help context menu");
            Console.WriteLine("quit                    terminate OrderTracker");
        }
    }
}
