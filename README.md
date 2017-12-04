# Accessing Existing Databases with Entity Framework Core

Not every .NET development project starts from scratch. Often you're rewriting applications to take advantage of different data sources, or to make connections to legacy data in more efficient ways. Entity Framework (EF) plays extremely well with existing databases, and it can generate some of the data-access code for you automatically. Using the practical techniques shown in this course, you will be able to use EF Core with existing relational databases, and modify the generated code as necessary. Richard Goforth shows how to connect to a database, scaffold a model from it, and begin improving on that model. He uses shadow properties, backing fields, inheritance relationships, concurrency tokens, and other techniques to best map a database to an application. No matter how untidy your tables and fields, EF will help you write clean, cross-platform code that is easy to maintain in the long run.

## Topics include:
Setting up your project<br/>
Connecting to a legacy database<br/>
Scaffolding an initial model and context<br/>
Improving the model<br/>
Updating properties and indexes<br/>
Adding concurrency tokens and timestamps<br/>
Creating complex relationships<br/>
Working with non-Microsoft databases such as SQLite and PostgreSQL<br/>

Source: https://www.lynda.com/Entity-Framework-tutorials

## How to generate concurrency error:<br/>

### A. In order to create concurrency token (in this example it will be [dbo].[Order].[LastUpdate]) please do the following:<br/>
1. Go to data model class ("HPlusSportsContext.cs")<br/>
2. Edit ".LastUpdate" property (add '.IsConcurrencyToken()'), see below:<br/>
```
entity.Property(e => e.LastUpdate)
                    .IsRequired()
                    .HasColumnType("timestamp")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate();
```

### NOTE: [LastUpdate] [timestamp] NOT NULL <br/><br/>

### B. In order to generate the error please do the following:<br/>
1. Wrrite code that updates some order, see below:<br/>
```
			//Get an order
            var lastOrder = context.Order.Last();
			
            //Update an order, set a new customer id
            lastOrder.CustomerId = 101;
			
            //Save changes into DB
            context.SaveChanges();
```

2. Set break-point on "context.SaveChanges()", see screenshot 1<br/>
3. Run the program<br/>
4. When the programm stops on break-point, run following quaery: "UPDATE [dbo].[Order] SET Status = 'canceled' WHERE OrderID = <order_id>;"
5. Continue to run the program<br/>
6. Verify an error message, see screenshot 3<br/>


### Screenshot 1:
![GUI](https://github.com/ikostan/AccessingExistingDatabasesWithEntityFrameworkCore/blob/master/Img/concurrency_error_1.PNG?raw=true "GUI screenshot")

### Screenshot 2:
![GUI](https://github.com/ikostan/AccessingExistingDatabasesWithEntityFrameworkCore/blob/master/Img/concurrency_error_2.PNG?raw=true "GUI screenshot")

### Screenshot 3:
![GUI](https://github.com/ikostan/AccessingExistingDatabasesWithEntityFrameworkCore/blob/master/Img/concurrency_error_3.PNG?raw=true "GUI screenshot")

## How to run migrations from CMD:<br/>
- Open CMD and navigate to project folder, for example: cd C:\Users\C#\AccessingExistingDatabasesWithEntityFrameworkCore\Project<br/>
- In order to update DB run: dotnet ef database update<br/>
- In order to update DB from specific migration run: dotnet ef database update <migration_name><br/>
- In order to create a new migration run following command: dotnet ef migrations add <migration_name><br/>
- To undo this action, use 'ef migrations remove'<br/>