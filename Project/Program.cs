using Project.EFClasses;
using System;
using System.Linq;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate context object
            var context = new HPlusSportsContext();

            //Run quaery in order to get all sales persons with the name starts from "s"
            var selespeople =
                context.Salesperson
                    .Where((s) => s.LastName.StartsWith("s"))
                    ;
            //Displays the results
            //selespeople.ToList().ForEach((p) => Console.WriteLine(p.FirstName + " " + p.LastName));
            selespeople.ToList().ForEach((p) => Console.WriteLine(p.FullName));
            Console.WriteLine();

            //Default value generation
            //Get initial data:
            var customer = context.Customer.First();            //Get first name from DB
            var salesperson = context.Salesperson.First();      //Get first name from DB
            var product = context.Product.First();              //Get first product from DB

            //Generate a new order
            var newOrder = new Order()
            {
                Customer = customer,
                Salesperson = salesperson,
                OrderItem = { new OrderItem() { Product = product} },
                OrderDate = DateTime.Now
            };

            //Save changes into DB
            context.Add(newOrder);
            context.SaveChanges();

            //Display the new order data:
            Console.WriteLine($"Order id: {newOrder.OrderId}, date: {newOrder.CreatedDate}");

            //Do not close cmd untill user press enter
            Console.ReadKey();
        }
    }
}
