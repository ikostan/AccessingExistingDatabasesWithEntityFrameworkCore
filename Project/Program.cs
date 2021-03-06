﻿using Project.EFClasses;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Project.SQLite;

namespace Project
{
    class Program
    {
        //DB model object
        private HPlusSportsContext context;// MS SQL
        private SQLiteDBContext contextSQLite;//SQLite

        //Color schema
        private ConsoleColor defaultBackGroundColor;
        private ConsoleColor defaultForeGroundColor;

        static void Main(string[] args)
        {
            //Instantiate context object
            Program prg = new Program();
            prg.context = new HPlusSportsContext(); //MS SQL context
            prg.contextSQLite = new SQLiteDBContext(); //SQLite context

            //prg.context.Database.Log = ((log) => Console.WriteLine(log));

            try
            {
                //prg.GetPersonsStartsFromS();

                //prg.GenerateNewOrder();

                //prg.UsingConcurrencyTokensSample();

                //prg.TestPerishable();

                //prg.NavigationDemo("CA");
                //prg.NavigationDemo(120);

                //prg.WorkWithComputedColumn();

                //prg.FirstChallange("MWRAS32");

                prg.LoadDataToSQLiteDB();

            }
            catch (Exception ex)
            {
                //Show error
                prg.DisplayError(ex);
            }

            //Do not close cmd untill user press enter
            Console.ReadKey();
        }

        /// <summary>
        /// Load data from MS SQL DB into SQLite DB
        /// </summary>
        private void LoadDataToSQLiteDB()
        {
            Console.WriteLine("Loading data into SQLite DB:\n");

            AddCustomers();
            AddSalesGroup();
            AddSalesperson();
            AddOrder();
            AddProduct();
            AddOrderItem();

            Console.WriteLine("Done\n");
        }

        /// <summary>
        /// Load data from OrderItem into OrderItemSQLite
        /// </summary>
        private void AddOrderItem()
        {
            Console.WriteLine("Loading data into SQLite >>> OrderItem");

            context.OrderItem.Load();

            foreach (OrderItem item in context.OrderItem.Local.ToList())
            {
                contextSQLite.Add(new OrderItemSQLite
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            contextSQLite.SaveChanges();
        }

        /// <summary>
        /// Load data from Product into ProductSQLite
        /// </summary>
        private void AddProduct()
        {
            Console.WriteLine("Loading data into SQLite >>> Product");

            context.Product.Load();

            foreach (Product item in context.Product.Local.ToList())
            {
                contextSQLite.Add(new ProductSQLite
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Size = item.Size,
                    Variety = item.Variety,
                    Price = item.Price,
                    Status = item.Status
                });
            }

            contextSQLite.SaveChanges();
        }

        /// <summary>
        /// Load data from Order into OrderSQLite
        /// </summary>
        private void AddOrder()
        {
            Console.WriteLine("Loading data into SQLite >>> Order");

            context.Order.Load();

            foreach (Order item in context.Order.Local.ToList())
            {
                //[OrderID], [OrderDate], [TotalDue], [Status], [CustomerID], [SalespersonID], [CreatedDate]

                contextSQLite.Add(new OrderSQLite
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    TotalDue = item.TotalDue,
                    Status = item.Status,
                    CustomerId = item.CustomerId,
                    SalespersonId = item.SalespersonId,
                    CreatedDate = item.CreatedDate
                });
            }

            contextSQLite.SaveChanges();
        }

        /// <summary>
        /// Load data from Salesperson into SalespersonSQLite
        /// </summary>
        private void AddSalesperson()
        {
            Console.WriteLine("Loading data into SQLite >>> Salesperson");

            context.Salesperson.Load();

            foreach (Salesperson item in context.Salesperson.Local.ToList())
            {
                contextSQLite.Add(new SalespersonSQLite
                {
                    SalespersonId = item.SalespersonId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Phone = item.Phone
                });
            }

            contextSQLite.SaveChanges();
        }

        /// <summary>
        /// Load data from SalesGroup into SalesGroupSQLite
        /// </summary>
        private void AddSalesGroup()
        {
            Console.WriteLine("Loading data into SQLite >>> SalesGroup");

            context.SalesGroup.Load();

            foreach (SalesGroup item in context.SalesGroup.Local.ToList())
            {
                contextSQLite.Add(new SalesGroupSQLite
                {
                    State = item.State,
                    Type = item.Type,
                });
            }

            contextSQLite.SaveChanges();
        }

        /// <summary>
        /// Load data from Customer into CustomerSQLite
        /// </summary>
        private void AddCustomers()
        {
            Console.WriteLine("Loading data into SQLite >>> Customer");

            context.Customer.Load();

            foreach (Customer item in context.Customer.Local.ToList())
            {
                contextSQLite.Add(new CustomerSQLite {CustomerId = item.CustomerId,
                                                        StrFldFirstName = item.StrFldFirstName,
                                                        StrFldLastName = item.StrFldLastName,
                                                        StrFldEmail = item.StrFldEmail,
                                                        StrFldPhone = item.StrFldPhone,
                                                        StrFldAddress = item.StrFldAddress,
                                                        StrFldCity = item.StrFldCity,
                                                        StrFldState = item.StrFldState,
                                                        StrFldZipcode = item.StrFldZipcode
                });
            }

            contextSQLite.SaveChanges();
        }

        /// <summary>
        /// Challenge:
        /// 
        /// Let's start in the main program and remove the code from previous materials. 
        /// Product MWRAS32 had just been recalled, and we need to notify all of the customers who purchased it. 
        /// Let's return a list of customer names, last comma first, with phone numbers to help perform this task. 
        /// For an extra challenge, sort the customers by last name and ensure that there are no duplicates in the list. 
        /// Product ID: "MWRAS32"
        /// 
        /// </summary>
        private void FirstChallange(string prdId)
        {
            var customers =
                context.Product
                    .Where((p) => p.ProductId == prdId)
                    .Join(context.OrderItem,
                            pid => pid.ProductId,
                            ord => ord.ProductId,

                            (pid, ord) => new { ProductId = pid.ProductId,
                                                OrderId = ord.OrderId,
                                                CustId = ord.Order.CustomerId }
                        )
                    .Join(context.Customer,
                            pid => pid.CustId,
                            cst => cst.CustomerId,
                            (pid, cst) => new { FirstName = cst.StrFldFirstName,
                                                LastName = cst.StrFldLastName,
                                                PhoneNum = cst.StrFldPhone }
                        )
                    .OrderBy((p) => p.LastName)
                    .Distinct()
                    .ToList()
                ;

            foreach (var item in customers)
            {
                Console.WriteLine($"{item.LastName}, {item.FirstName} {item.PhoneNum}");
            }

            //Some internal test
            //var products =
            //    context.Order
            //        .Where((c) => c.Customer.StrFldLastName == "Adams" && c.Customer.StrFldFirstName == "Shawn")
            //        .Join(context.OrderItem,
            //        ord => ord.OrderId,
            //        ordItm => ordItm.OrderId,
            //        (ord, ordItm) => new { ordItm.Product.ProductId }
            //    )
            //    ;

            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId}");
            //}

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
                    Console.WriteLine($"{e.Variety,-10} | {e.ProductId,-8} | {e.ProductName,-8} | {e.ExpirationDays,10} |"));
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
        private void DisplayError(Exception ex)
        {
            ChangeDefaultColorSchema();

            Console.WriteLine($"\n{ex.Message}\n");
            Console.WriteLine($"\n{ex.StackTrace}\n");
            Console.WriteLine($"\nInner exception: \n{ex.InnerException}\n");

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
