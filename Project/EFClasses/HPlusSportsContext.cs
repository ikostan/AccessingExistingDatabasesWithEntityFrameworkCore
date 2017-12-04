using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.EFClasses
{
    public partial class HPlusSportsContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SalesGroup> SalesGroup { get; set; }
        public virtual DbSet<Salesperson> Salesperson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HPlusSports;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CmpLastFirst)
                    .HasColumnName("cmp_LastFirst")
                    .HasColumnType("varchar(102)")
                    .HasComputedColumnSql("(([str_fld_LastName]+', ')+[str_fld_FirstName])");

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

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderDate)
                    .HasName("IX_Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LastUpdate)
                    .IsRequired()
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("('none')");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.Salesperson)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.SalespersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Order_Salesperson");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrderItem_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrderItem_Product1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(10);

                entity.Property(e => e.Perishable).HasDefaultValueSql("((0))");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName).HasColumnType("varchar(50)");

                entity.Property(e => e.Status).HasColumnType("varchar(50)");

                entity.Property(e => e.Variety).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<SalesGroup>(entity =>
            {
                entity.HasIndex(e => new { e.State, e.Type })
                    .HasName("IX_StateType")
                    .IsUnique();

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<Salesperson>(entity =>
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

                entity.Property(e => e.SalesGroupState)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasDefaultValueSql("(N'CA')");

                entity.Property(e => e.SalesGroupType).HasDefaultValueSql("((1))");



                entity.Ignore(e => e.FirstName); //Ignore this field (was created manualy) while maping to DB

                entity.HasOne(d => d.SalespersonNavigation)
                    .WithOne(p => p.InverseSalespersonNavigation)
                    .HasForeignKey<Salesperson>(d => d.SalespersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Salesperson_Salesperson");

            });
        }
    }
}