
//Entity Classes   Product(ID,Name,Price,Stock)=>Product(ID,Name,Price,Stock)
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

class Program
{


    static void Main(string[] args)
    {
        // InsertUsers();
        // InsertAddreses();
        // using (var db = new ShopContext())
        // {

        //     var user = db.Users.FirstOrDefault(i => i.UserName == "berk");
        //     if(user!=null)
        //     {
        //         user.Addresses=new List<Address>();
        //         user.Addresses.Add(new Address(){
        //             FullAddress="Antalya",
        //             Title="Tatil",
        //             Body="Tatil Adresi",
        //         });
        //         db.SaveChanges();
        //     }

        // }
        // using (var db = new ShopContext())
        // {
        //     var customer =new Customer()
        //     {
        //         IdentityNumber="123456789",
        //         FirstName="Berk",
        //         LastName="Kara",
        //         UserId=1

        //     };
        //     db.Customers.Add(customer);
        //     db.SaveChanges();
        //     var user = new User()
        //     {
        //         UserName = "Rana",
        //         Email = "rana@gmail.com",
        //         Customer=new Customer()
        //         {
        //             IdentityNumber="3322211",
        //             FirstName="Rana",
        //             LastName="Göker",
        //         }
        //     };
        //     db.Users.Add(user);
        //     db.SaveChanges();
        // }

        // using (var db =new ShopContext())
        // {
        //     var products= new List<Product>()
        //     {
        //         new Product(){Name="Samsung S5",Price=1200},
        //         new Product(){Name="Samsung S6",Price=1400},
        //         new Product(){Name="Samsung S7",Price=1600}

        //     };
        //     db.Products.AddRange(products);
        //     var categories=new List<Category>()
        //     {
        //         new Category(){Name="Telefon"},
        //         new Category(){Name="Bilgisayar"},
        //         new Category(){Name="Elektronik"}
        //     };
        //     db.Categories.AddRange(categories);
        //     db.SaveChanges();

        // }
        // using (var db = new ShopContext())
        // {
        //     int[] ids = new int[2] { 2, 3 };
        //     var p = db.Products.Find(1);
        //     p.ProductCategories = ids.Select(i => new ProductCategory()
        //     {
        //         ProductId = p.Id,
        //         CategoryId = i
        //     }).ToList();
        //     db.SaveChanges();
        // }
        //    using (var db =new ShopContext())
        //    {
        //     var p=new Product()
        //     {
        //         Name="Samsung S8",
        //         Price=2000,
        //     };
        //     db.Products.Add(p);
        //     db.SaveChanges();
        //    }
        DataSeeding.Seed(new ShopContext());

    }
    public class DataSeeding
    {
        public static void Seed(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context is ShopContext)
                {
                    ShopContext _context = context as ShopContext;
                    if (_context.Products.Count() == 0)
                    {
                        _context.Products.AddRange(Products);

                    }
                    if (_context.Categories.Count() == 0)
                    {
                        _context.Categories.AddRange(Categories);

                    }
                }
                context.SaveChanges();
            }
        }
        private static Product[] Products ={
            new Product(){Name="Samsung S5",Price=1200},
            new Product(){Name="Samsung S6",Price=1400},
            new Product(){Name="Samsung S7",Price=1600}

        };
        private static Category[] Categories ={
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Elektronik"}
        };


    }
    static void InsertUsers()
    {
        var users = new List<User>(){
            new User(){
                UserName="Eren",
                Email="eren@gmail.com",
            },
            new User(){
                UserName="Berat",
                Email="berat@gmail.com",
            },new User(){
                UserName="berk",
                Email="berk@gmail.com",
            },new User(){
                UserName="sıla",
                Email="sıla@gmail.com",
            },
        };
        using (var db = new ShopContext())
        {
            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }
    static void InsertAddreses()
    {
        var addreses = new List<Address>(){
            new Address(){
                FullAddress="İstanbul",
                Title="İş",
                Body="İş Adresi",
                UserId=1
            }, new Address(){
                FullAddress="Ankara",
                Title="Ev",
                Body="Ev Adresi"    ,
                UserId=1

            }, new Address(){
                FullAddress="Balıkesir",
                Title="Okul",
                Body="Okul Adresi"  ,
                UserId=1

            }
        };
        using (var db = new ShopContext())
        {
            db.Addresses.AddRange(addreses);
            db.SaveChanges();
        }
    }


    public class Customer
    {
        [Column("Customer_Id")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
    public class Supplier
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Address> Addresses { get; set; }
        public Customer Customer { get; set; }

    }
    public class Address
    {
        public int Id { get; set; }
        public string FullAddress { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
    static void DeleteProduct(int id)
    {
        using (var db = new ShopContext())
        {
            var p = db.Products.Where(i => i.Id == id).FirstOrDefault();
            if (p == null)
            {
                System.Console.WriteLine("Product Not Found");
                return;
            }
            else
            {
                db.Products.Remove(p);
                db.SaveChanges();
                System.Console.WriteLine("Product Deleted");

            }
        }

    }
    static void UpdateProduct()
    {

        using (var db = new ShopContext())
        {
            var p = db.Products.Where(i => i.Id == 2).FirstOrDefault();
            System.Console.WriteLine($"{p.Name}-{p.Price}");
            if (p == null)
            {
                System.Console.WriteLine("Product Not Found");
                return;
            }
            else
            {
                p.Price = 2400;
                p.Name = "Msi Laptop";
                db.Products.Update(p);
                db.SaveChanges();
                System.Console.WriteLine("Product Updated");
            }



        }
    }
    static void getProductById(int id)
    {
        using (var db = new ShopContext())
        {
            var p = db.Products.Where(p => p.Id == id).FirstOrDefault();
            System.Console.WriteLine($"{p.Name}-{p.Price}");
        }
    }
    static void getProductByName(string Name)
    {
        using (var db = new ShopContext())
        {
            //tek bir kayıt döndürür
            // var p = db.Products.Where(p => p.Name == Name).FirstOrDefault();
            var p = db.Products.Where(p => p.Name.ToLower().Contains(Name.ToLower())).ToList();
            foreach (var a in p)
            {
                System.Console.WriteLine($"{a.Name}-{a.Price}");
            }
        }
    }
    static void getAllProducts()
    {
        using (var db = new ShopContext())
        {
            var products = db.Products
            //kolon seçimi yapmak için
            // .Select(p=>new {
            //     p.Name,
            //     p.Price
            // })
            .ToList();
            foreach (var p in products)
            {
                System.Console.WriteLine($"{p.Name}-{p.Price}");
            }
        }
    }
    static void AddProducts()
    {
        using (var db = new ShopContext())
        {
            var products = new List<Product>()
          {
            new Product{Name="Asus",Price=1200},
            new Product{Name="Dell",Price=900},
            new Product{Name="Monster",Price=1000},
          };
            db.Products.AddRange(products);
            db.SaveChanges();
            System.Console.WriteLine("Products Added");
        }
    }
    static void AddProduct()
    {
        using (var db = new ShopContext())
        {
            var p = new Product { Name = "Asus ZenBook", Price = 1450 };
            db.Products.Add(p);
            db.SaveChanges();
            System.Console.WriteLine("Product Added");
        }
    }
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //dotnet add package Microsoft.EntityFrameworkCore.Sqlite
        //dotnet add package Microsoft.Extensions.Logging.Console
        //Veri tabanına eklerken sorguyu görmek için
        public static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;user=root;password=Erenjr;database=ShopDb";
            var serverVersion = new MySqlServerVersion(new Version(7, 0, 0));
            optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
           .UseMySql(connectionString, serverVersion);

            //  .UseSqlite("Data Source=shop.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

            modelBuilder.Entity<Product>()
            .ToTable("Urunler");

            modelBuilder.Entity<Customer>()
            .Property(p => p.IdentityNumber)
            .HasMaxLength(11)
            .IsRequired();

            modelBuilder.Entity<ProductCategory>()
            .HasKey(t => new { t.ProductId, t.CategoryId });

            modelBuilder.Entity<ProductCategory>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductCategory>()
            .HasOne(pt => pt.Category)
            .WithMany(t => t.ProductCategories)
            .HasForeignKey(pt => pt.CategoryId);


        }

    }
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime InsertedDate { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public List<ProductCategory> ProductCategories { get; set; }

    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

    }
    // [NotMapped]
    [Table("ProductCategories")]
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
//NOTES
//dotnet tool install --global dotnet-ef
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet ef migrations add InitialCreate
// dotnet ef database update
//dotnet run