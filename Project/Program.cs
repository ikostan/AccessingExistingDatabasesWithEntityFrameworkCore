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
            selespeople.ToList().ForEach((p) => Console.WriteLine(p.FirstName + " " + p.LastName));

            //Do not close cmd untill user press enter
            Console.ReadKey();
        }
    }
}
