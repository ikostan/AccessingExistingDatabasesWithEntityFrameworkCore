using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.SQLite
{
    public partial class HPlusSportsContextSQLite : DbContext
    {
        public virtual DbSet<CustomerSQLite> CustomerSQLite { get; set; }
        public virtual DbSet<OrderSQLite> OrderSQLite { get; set; }
        public virtual DbSet<OrderItemSQLite> OrderItemSQLite { get; set; }
        public virtual DbSet<SalesGroupSQLite> SalesGroupSQLite { get; set; }
        public virtual DbSet<SalespersonSQLite> SalespersonSQLite { get; set; }
        public virtual DbSet<ProductSQLite> ProductSQLite { get; set; }

        //PerishableProduct is custom class, extends from Product
        public virtual DbSet<PerishableProductSQLite> PerishableProductSQLite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HPlusSports;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerSQLite>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                //Not supported by SQLite
                //entity.Property(e => e.CmpLastFirst)
                //    .HasColumnName("cmp_LastFirst")
                //    .HasColumnType("varchar(102)")
                //    .HasComputedColumnSql("(([str_fld_LastName]+', ')+[str_fld_FirstName])")
                //    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.StrFldAddress)
                    .HasColumnName("str_fld_Address")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.StrFldCity)
                    .HasColumnName("str_fld_City")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.StrFldEmail)
                    .HasColumnName("str_fld_Email")
                    .HasColumnType("varchar(300)")
                    .HasAnnotation("BackingField", "_email");

                entity.Property(e => e.StrFldFirstName)
                    .HasColumnName("str_fld_FirstName")
                    .HasColumnType("varchar(50)");

                //Indexing on last name
                entity.HasIndex((e) => e.StrFldLastName);

                entity.Property(e => e.StrFldLastName)
                    .HasColumnName("str_fld_LastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.StrFldPhone)
                    .HasColumnName("str_fld_Phone")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.StrFldState)
                    .HasColumnName("str_fld_State")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.StrFldZipcode)
                    .HasColumnName("str_fld_Zipcode")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<OrderSQLite>(entity =>
            {
                entity.HasIndex(e => e.OrderDate)
                    .HasName("IX_Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                //Not supported by SQLite
                //entity.Property(e => e.LastUpdate)
                //    .IsRequired()
                //    .HasColumnType("timestamp")
                //    .IsConcurrencyToken()
                //    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("('none')");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.HasOne(d => d.CustomerSQLite)
                    .WithMany(p => p.OrderSQLite)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.SalespersonSQLite)
                    .WithMany(p => p.OrderSQLite)
                    .HasForeignKey(d => d.SalespersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Order_Salesperson");
            });

            modelBuilder.Entity<OrderItemSQLite>(entity =>
            {
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(10);

                entity.HasOne(d => d.OrderSQLite)
                    .WithMany(p => p.OrderItemSQLite)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrderItem_Order");

                entity.HasOne(d => d.ProductSQLite)
                    //.WithMany(p => p.OrderItem) //commented out since this navigation is no longer exist. Replaced by empty .WithMany() navigation
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrderItem_Product1");
            });

            modelBuilder.Entity<ProductSQLite>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(10);

                //Must be added manualy cince we separated between Perishable and Product
                entity.HasDiscriminator<bool>("Perishable")
                    .HasValue<ProductSQLite>(false)
                    .HasValue<PerishableProductSQLite>(true)
                ;

                //See Discriminator above (no longer need this definition)
                //entity.Property(e => e.Perishable).HasDefaultValueSql("((0))");

                //Default value for this property
                //Added in order not to include it into migration model
                entity.Property<bool>("Perishable").HasDefaultValueSql("0");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName).HasColumnType("varchar(50)");

                entity.Property(e => e.Status).HasColumnType("varchar(50)");

                entity.Property(e => e.Variety).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<SalesGroupSQLite>(entity =>
            {
                entity.HasIndex(e => new { e.State, e.Type })
                    .HasName("IX_StateType")
                    .IsUnique();

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                //Relationships between salesperson and SalesGroup. 
                //Has principal key instead of FK since there is no actual relationships between those tables.
                entity.HasMany((e) => e.SalesPeopleSQLite)
                .WithOne((s) => s.SalesGroupSQLite)
                .HasPrincipalKey((o) => new { o.State, o.Type});
            });

            modelBuilder.Entity<SalespersonSQLite>(entity =>
            {
                entity.Property(e => e.SalespersonId)
                    .HasColumnName("SalespersonID")
                    .ValueGeneratedOnAdd();

                //Shadow properties:

                //entity.Property(e => e.Address).HasColumnType("varchar(50)");
                entity.Property<string>("Address").HasColumnType("varchar(50)");

                //entity.Property(e => e.City).HasColumnType("varchar(50)");
                entity.Property<string>("City").HasColumnType("varchar(50)");

                //entity.Property(e => e.State).HasColumnType("varchar(50)");
                entity.Property<string>("State").HasColumnType("varchar(50)");

                //entity.Property(e => e.Zipcode).HasColumnType("varchar(50)");
                entity.Property<string>("Zipcode").HasColumnType("varchar(50)");


                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(50)");

                entity.Property(e => e.LastName).HasColumnType("varchar(50)");

                entity.Property(e => e.Phone).HasColumnType("varchar(50)");

                //T-SQL format:
                //entity.Property(e => e.SalesGroupState)
                //    .IsRequired()
                //    .HasMaxLength(2)
                //    .HasDefaultValueSql("(N'CA')");

                //SQLite format:
                entity.Property(e => e.SalesGroupState)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasDefaultValue("CA");

                //T-SQL format:
                //entity.Property(e => e.SalesGroupType).HasDefaultValueSql("((1))");

                //SQLite format:
                entity.Property(e => e.SalesGroupType).HasDefaultValueSql("1");

                entity.Ignore(e => e.FirstName); //Ignore this field (was created manualy) while maping to DB

                //This navigation key is removed because it points to himself
                //entity.HasOne(d => d.SalespersonNavigation)
                //    .WithOne(p => p.InverseSalespersonNavigation)
                //    .HasForeignKey<Salesperson>(d => d.SalespersonId)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasConstraintName("FK_Salesperson_Salesperson");

            });
        }
    }
}