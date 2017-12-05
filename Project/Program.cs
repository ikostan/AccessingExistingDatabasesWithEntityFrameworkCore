using Project.EFClasses;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Project
{
    class Program
    {
        //DB model object
        private HPlusSportsContext context;

        //Color schema
        private ConsoleColor defaultBackGroundColor;
        private ConsoleColor defaultForeGroundColor;

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

                //prg.TestPerishable();

                //prg.NavigationDemo("CA");
                //prg.NavigationDemo(120);

                //prg.WorkWithComputedColumn();

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
        /// How to work with computed column example
        /// </summary>
        private void WorkWithComputedColumn()
        {
            //Get calculated name and display it
            var customer = context.Customer.Skip(2).First();
            Console.WriteLine($"{customer.CmpLastFirst}");

            //Preserve original name
            string original = customer.StrFldFirstName;

            //Change first name + do not save changes and display calculated name again
            customer.StrFldFirstName = "Pueblo";
            Console.WriteLine($"{customer.CmpLastFirst}");

            //Save changes and display calculated name
            context.SaveChanges();
            Console.WriteLine($"{customer.CmpLastFirst}");

            //Get back to original name
            customer.StrFldFirstName = original;
            context.SaveChanges();

            //Change calculated field, save changes and display calculated name again => invalid operation
            //Fuxed by setting SET property as private inside Customer model
            //customer.CmpLastFirst = "First Last";
            //context.SaveChanges();
            //Console.WriteLine($"{customer.CmpLastFirst}");
        }

        /// <summary>
        /// Navigation between entities.
        /// Filter by salesperson id
        /// In this method we will connect product with orders via sales-person.
        /// </summary>
        private void NavigationDemo(int id)
        {
            //Header
            ChangeDefaultColorSchema();
            Console.WriteLine("Get all orders done by salesperson with a specific id");
            SetDefaultColorSchema();

            //Get all orders done by salesperson from a specific state
            var orders =
                context.Order
                    .Include((o) => o.OrderItem)
                    .ThenInclude((oi) => oi.Product)
                    .Where((p) => p.SalespersonId == id)
                    .ToList()
                ;

            //Show the results:
            orders.ForEach((e) => Console.WriteLine($"id: {e.OrderId}"));
        }

        /// <summary>
        /// Navigation between entities.
        /// Filter by State
        /// In this method we will connect product with orders via sales-person.
        /// </summary>
        private void NavigationDemo(string state)
        {
            //Header
            ChangeDefaultColorSchema();
            Console.WriteLine("Get all orders done by salesperson from specific state:");
            SetDefaultColorSchema();

            //Get all orders per specific salseperson id
            var orders =
                context.Order
                    .Include((o) => o.OrderItem)
                    .ThenInclude((oi) => oi.Product)
                    .Where((p) => p.Salesperson.SalesGroupState == state)
                    .ToList()
                ;

            //Show the results:
            orders.ForEach((e) => Console.WriteLine($"id: {e.OrderId}"));
        }

        /// <summary>
        /// Test PerishableProduct class 
        /// </summary>
        private void TestPerishable()
        {
            //Get all Perishable Products from DB and convert data collection to list:
            var perishableProduct = context.PerishableProduct.ToList();

            //Change default color schema:
            ChangeDefaultColorSchema();

            //Display header:
            Console.WriteLine(String.Format("{0,-10} | {1,-8} | {2,-8} | {3,-10} |", "variety", "id", "name", "expiration"));

            //Change back to default colors:
            SetDefaultColorSchema();

            Console.WriteLine($"-----------------------------------------------");

            //Loop in order to display:
            perishableProduct
                .ForEach((e) => 
                    Console.WriteLine($"{e.Variety, -10} | {e.ProductId, -8} | {e.ProductName, -8} | {e.ExpirationDays, 10} |"));
        }

        /// <summary>
        /// Change back to default colors
        /// </summary>
        private void SetDefaultColorSchema()
        {
            Console.ForegroundColor = defaultForeGroundColor;
            Console.BackgroundColor = defaultBackGroundColor;
        }

        /// <summary>
        /// Change default color schema
        /// </summary>
        private void ChangeDefaultColorSchema()
        {
            defaultBackGroundColor = Console.BackgroundColor;
            defaultForeGroundColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        /// <summary>
        /// Display error (red text on yellow background)
        /// </summary>
        /// <param name="error"></param>
        private void DisplayError(string error)
        {
            ChangeDefaultColorSchema();

            Console.WriteLine($"\n{error}\n");

            SetDefaultColorSchema();
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
