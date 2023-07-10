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

        private StreamReader OrderStream;
        public List<Order> OrderStack;

        public Orders()
        {
            OrderStack = new List<Order>();          
        }

        public int AddOrder(Order order) 
        {
            try
            {
                OrderStack.Add(order);
            } catch 
            {
                return -1;
            }

            return 1;
        }

        public int ParseOrderFile(String path)
        {
            String line, lti, token ;
            Order currentOrder = null;
            int numericToken;
            double decimalToken;
            int numerror = 0, totalerror = 0, numsuccess = 0;

            try
            {
                OrderStream = new StreamReader(path);

                line = OrderStream.ReadLine();
                while (line != null)
                {

                    lti = line.Substring(0, 3);                         
                    switch (lti)
                    {
                        case "100":
                            token = "";
                            if (currentOrder != null)
                            {
                                AddOrder(currentOrder);

                                if (numerror == 0)
                                {
                                    currentOrder.Success = true;
                                    numsuccess += 1;
                                    Console.WriteLine("Order number " + currentOrder.orderNumber + " inserted without errors");
                                }
                                else
                                {
                                    Console.WriteLine("Order number " + currentOrder.orderNumber + " inserted with errors");
                                    totalerror += numerror;
                                }

                                numerror = 0;
                            }

                            currentOrder = new Order();
                            try
                            {
                                token = line.Substring(3, 10);
                                numericToken = Convert.ToInt32(token);
                                currentOrder.orderNumber = numericToken;

                                token = line.Substring(13, 5);
                                numericToken = Convert.ToInt32(token);
                                currentOrder.totalItems = numericToken;

                                token = line.Substring(18, 10);
                                decimalToken = Convert.ToDouble(token);
                                currentOrder.totalCost = decimalToken;

                                token = line.Substring(28, 19);
                                currentOrder.orderDate = token;

                                token = line.Substring(47, 50);
                                currentOrder.OrderCustomer.CustomerName = token;

                                token = line.Substring(97, 30);
                                currentOrder.OrderCustomer.CustomerPhone = token;

                                token = line.Substring(127, 50);
                                currentOrder.OrderCustomer.CustomerEmail = token;

                                token = line.Substring(177, 1);
                                numericToken = Convert.ToInt32(token);
                                currentOrder.isPaid = numericToken;

                                token = line.Substring(178, 1);
                                currentOrder.isShipped = numericToken;

                                token = line.Substring(179, 1);
                                currentOrder.isCompleted = numericToken;

                            }
                            catch (Exception ex)
                            {
                                numerror++;
                                currentOrder.Success = false;
                                Console.WriteLine("Invalid Order Parameter - " + token);
                                currentOrder.ErrorMsg = ex.Message;
                            }

                            break;
                        case "200":
                            token = String.Empty;
                            try
                            {
                                token = line.Substring(3, 50);
                                currentOrder.OrderCustomer.Addressl1 = token;

                                token = line.Substring(53, 50);
                                currentOrder.OrderCustomer.Addressl2 = token;

                                token = line.Substring(103, 50);
                                currentOrder.OrderCustomer.City = token;

                                token = line.Substring(153, 2);
                                currentOrder.OrderCustomer.State = token;

                                token = line.Substring(155, 10);
                                currentOrder.OrderCustomer.Zip = token;

                            }
                            catch (Exception e)
                            {
                                numerror++;
                                currentOrder.Success = false;
                                Console.WriteLine("Invalid Address Parameter - " + token);
                                currentOrder.ErrorMsg = e.Message;
                            }

                            break;
                        case "300":
                            Item item = new Item();
                            token = String.Empty;
                            try
                            {
                                token = line.Substring(3, 2);
                                numericToken = Convert.ToInt32(token);
                                item.lineNo = numericToken;

                                token = line.Substring(5, 5);
                                numericToken = Convert.ToInt32(token);
                                item.quantity = numericToken;

                                token = line.Substring(10, 10);
                                decimalToken = Convert.ToDouble(token);
                                item.costEach = decimalToken;

                                token = line.Substring(20, 10);
                                decimalToken = Convert.ToDouble(token);
                                item.costSum = decimalToken;

                                token = line.Substring(30, 50);
                                item.ItemDescription = token;

                                currentOrder.AddLineItem(item);
                            }
                            catch (Exception ex) 
                            {
                                currentOrder.Success = false;
                                Console.WriteLine("Invalid Order Detail Parameter - " + token);
                                numerror++;
                                currentOrder.ErrorMsg = ex.Message;
                            }
                            break;
                        default:
                            break;
                    }

                    line = OrderStream.ReadLine();
                }
                if (currentOrder != null)
                {
                    AddOrder(currentOrder);

                    if (numerror == 0)
                    {
                        Console.WriteLine("Order number " + currentOrder.orderNumber + " inserted without errors");
                        numsuccess += 1;
                    }
                    else
                    {
                        Console.WriteLine("Order number " + currentOrder.orderNumber + " inserted with errors");
                        totalerror += numerror;
                    }

                    numerror = 0;
                }
                OrderStream.Close();
            } catch (IOException e) 
            {
                Console.WriteLine("Invalid File Handle : " + e);
            }
            catch (OutOfMemoryException e) 
            {
                Console.WriteLine("Please be gentle : " + e);
            }
            catch ( ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Parse Orders Completed - Total Sucessess: " + numsuccess + " Total Failures: " + totalerror);
            
            return 1;
        }

    }

   
}
