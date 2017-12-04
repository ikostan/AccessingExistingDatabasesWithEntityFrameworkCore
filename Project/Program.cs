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

            

            //Do not close cmd untill user press enter
            Console.ReadKey();
        }

        /// <summary>
        /// get all sales persons with the name starts from "s"
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
            Console.WriteLine();
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
