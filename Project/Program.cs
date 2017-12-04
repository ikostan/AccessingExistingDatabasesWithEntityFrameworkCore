using Project.EFClasses;
using System;
using System.Linq;

namespace Project
{
    class Program
    {
        //DB model object
        private HPlusSportsContext context;

        static void Main(string[] args)
        {
            //Instantiate context object
            Program prg = new Program();
            prg.context = new HPlusSportsContext();

            try
            {
                //prg.GetPersonsStartsFromS();

                //prg.GenerateNewOrder();

                //prg.UsingConcurrencyTokensSample();


            }
            catch (Exception ex)
            {
                //Show error
                prg.DisplayError(ex.Message);
            }

            //Do not close cmd untill user press enter
            Console.ReadKey();
        }

        /// <summary>
        /// Display error (red text on yellow background)
        /// </summary>
        /// <param name="error"></param>
        private void DisplayError(string error)
        {
            ConsoleColor defaultBackGroundColor = Console.BackgroundColor;
            ConsoleColor defaultForeGroundColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n{error}\n");

            Console.ForegroundColor = defaultForeGroundColor;
            Console.BackgroundColor = defaultBackGroundColor;
        }

        /// <summary>
        /// Sample of usage concurrency tokens and timestamps
        /// Show case for concurrency conflict
        /// </summary>
        private void UsingConcurrencyTokensSample()
        {
            //Get an order
            var lastOrder = context.Order.Last();

            //Display original data
            Console.WriteLine($"Original customer id: {lastOrder.CustomerId}");

            //Update an order, set a new customer id
            lastOrder.CustomerId = lastOrder.CustomerId - 1;

            //Save changes into DB
            context.SaveChanges();

            //Display updated data
            Console.WriteLine($"Updated customer id: {lastOrder.CustomerId}");
        }

        /// <summary>
        /// Get all sales persons with the name starts from "s"
        /// </summary>
        /// <param name="context"></param>
        private void GetPersonsStartsFromS()
        {
            //Run quaery in order to get all sales persons with the name starts from "s"
            var selespeople =
                context.Salesperson
                    .Where((s) => s.LastName.StartsWith("s"))
                    ;

            //Displays the results
            //selespeople.ToList().ForEach((p) => Console.WriteLine(p.FirstName + " " + p.LastName));
            selespeople.ToList().ForEach((p) => Console.WriteLine(p.FullName));
        }

        /// <summary>
        /// Generate a new order using default value for DateTime
        /// </summary>
        private void GenerateNewOrder()
        { 
            //Default value generation
            //Get initial data:
            var customer = context.Customer.First();            //Get first name from DB
            customer.StrFldEmail = "CAPITAL@LETTERS.COM";

            var salesperson = context.Salesperson.First();      //Get first name from DB
            var product = context.Product.First();              //Get first product from DB

            //Generate a new order
            var newOrder = new Order()
            {
                Customer = customer,
                Salesperson = salesperson,
                OrderItem = { new OrderItem() { Product = product } },
                OrderDate = DateTime.Now
            };

            //Save changes into DB
            context.Add(newOrder);
            context.SaveChanges();

            //Display the new order data:
            Console.WriteLine($"Order id: {newOrder.OrderId}, date: {newOrder.CreatedDate}");
        }

        //End of class
    }
}
