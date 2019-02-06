
using OrderClient.Models;
using OrderClient.Services;

namespace OrderClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        private static OrderService _service;

        private const string Quit = "q";
        private const string Delete = "2";
        private const string New = "1";
        private const string ListAllTweets = "a";
        private const string Yes = "y";

        static void Main(string[] args)
        {
            _service = new OrderService();
            string userInput = "";

            while (userInput.ToUpper() != Quit)
            {
                Console.WriteLine("> a < Orders");
                Console.WriteLine("> 1 < Place Order");
                Console.WriteLine("> 2 < Delete Order");
                Console.WriteLine("> q < Exit!");
                Console.Write("Select action ");

                userInput = Console.ReadLine().ToUpper();
                int order = default(int);

                if (userInput == ListAllTweets)
                {
                    ShowOrders();
                }
                else if (userInput == New)
                {
                    AddOrder();
                }
                else if (Int32.TryParse(userInput, out order))
                {
                    ShowOrders(order);
                }
            }
        }

        private static void ShowOrders(int orderId)
        {
            Order o = _service.GetOrderByID(orderId);
            Console.WriteLine("Product name: {0}), product quantity: {1} ", o.name, o.quantity);
            Console.WriteLine("Product date: {0}", o.date);
            Console.WriteLine("");
            Console.WriteLine("> 2 < Delete Order");
            Console.WriteLine("> q < Exit!");
            Console.Write("Select action ");

            var userInput = Console.ReadLine().ToUpper();
            if (userInput == Delete)
            {
                DeleteOrder(orderId);
            }
        }

        private static void EditOrder(int orderId)
        {
            Console.WriteLine("");
            Console.Write("Product name ");
            var name = Console.ReadLine();

            Console.Write("Product quatity ");
            var q = Console.ReadLine();

            Order o = new Order
            {
                id = orderId,
                name = name,
                date = q
            };

            _service.UpdateOrder(o);
        }

        private static void AddOrder()
        {
            Console.WriteLine("");
            Console.Write("Product name ");
            var name = Console.ReadLine();
            Console.Write("Product quantity ");
            var quantity = Console.ReadLine();
            Console.Write("Product date ");
            var date = Console.ReadLine();

            Order o = new Order
            {
                name = name,
                quantity = quantity,
                date = date
     
            };
            _service.CreateOrder(o);
        }

        static void ShowOrders()
        {
            IList<Order> order = _service.GetOrders();

            foreach (Order o in order)
            {
                Console.WriteLine("Orders from DB");
                Console.WriteLine( o.id);
                Console.WriteLine(o.name);
                Console.WriteLine(o.quantity);
                Console.WriteLine(o.date);
            }
            Console.WriteLine("");
        }

        private static void DeleteOrder(int orderId)
        {
            Console.WriteLine("");
            Console.Write("Do you want to delete this order? Pres y for Yes. ");
            Console.WriteLine("");

            var userInput = Console.ReadLine().ToUpper();
            if (userInput == Yes)
            {
                _service.DeleteOrder(orderId);
            }
        }
    }
}
