using OrderTracker.src.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTracker.src.Model
{

    public class Orders : Order
    {

        public OrderReader orderReader;

        public List<Order> OrderStack;

        public Orders(OrderReader reader)
        {
            OrderStack = new List<Order>();  
            orderReader = reader;
        }

        public int AddOrder(Order order) 
        {
            try
            {
                
            } catch 
            {
                return -1;
            }

            return 1;
        }

        public int ParseOrderFile(String path)
        {
            String line;
            Order currentOrder = null;
            int numerror = 0, totalerror = 0, numsuccess = 0;

            StreamReader OrderStream = new StreamReader(path);

                // read through file line-by-line
                while ((line = OrderStream.ReadLine()) != null)
                {

                    // switching Line Type Identifier                      
                    switch (line.Substring(0, 3))
                    {
                        case "100":
                            
                            if (orderReader.isVerbose)
                                Console.WriteLine("Creating Order...");

                            currentOrder = new Order();

                            if (orderReader.isVerbose)
                                Console.WriteLine("Parsing Order...");

                            if (currentOrder.ParseOrder(line) == false)
                            {
                                numerror++;
                                Console.WriteLine("Parse Order error - ");
                                if (orderReader.isVerbose)
                                {
                                    Console.WriteLine(" Error Log - ");
                                    Console.WriteLine(currentOrder.ErrorMsg + "\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Parse Order success ");
                            }

                            OrderStack.Add(currentOrder);
                            if (orderReader.isVerbose)
                               Console.WriteLine("Adding to Order Stack...");

                            
                           if (numerror == 0)
                           {
                               currentOrder.Success = true;
                               numsuccess += 1;
                           }
                           else
                           {
                               Console.WriteLine("Order number " + currentOrder.orderNumber + " inserted with errors");
                               totalerror += numerror;
                           }

                           numerror = 0;
                            
                            break;
                        case "200":
                            if (orderReader.isVerbose)
                                Console.WriteLine("Parsing Customer Address...");

                            // Parse Address from line
                            if(currentOrder.OrderCustomer.ParseAddress(line) == false)
                            {
                                numerror += 1;

                                if (orderReader.isVerbose)
                                {
                                    Console.WriteLine("Parse Address error - Error Log - ");
                                    Console.WriteLine(currentOrder.ErrorMsg + "\n");
                                }
                            }
                            
                            if (numerror == 0)
                            {
                                currentOrder.OrderCustomer.Success = true;
                                numsuccess += 1;   
                            }
                            else
                            {
                                totalerror += numerror;
                            }



                            break;
                        case "300":
                            LineItem lineItem = new LineItem();
                            
                            if(lineItem.ParseLineItem(line) == false)
                            {
                                numerror += 1;
                                if (orderReader.isVerbose)
                                {
                                    Console.WriteLine("Parse Line Item error - Error Log - ");
                                    Console.WriteLine(lineItem.ErrorMsg + "\n");
                                }
                            }

                            currentOrder.LineItems.Add(lineItem);

                            break;
                        default:
                            break;
                    }

                   
                }                       
               
                OrderStream.Close();
            

            Console.WriteLine("Parse Orders Completed - Total Sucessess: " + numsuccess + " Total Failures: " + totalerror);
            
            return 1;
        }

    }

   
}
