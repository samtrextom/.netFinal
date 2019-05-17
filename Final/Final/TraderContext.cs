using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class TraderContext : DbContext
    {
        public TraderContext() : base("name=TraderContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void AddProduct(Product product)
        {
            this.Products.Add(product);
            this.SaveChanges();
        }

        public void AddCategory(Category category)
        {
            this.Categories.Add(category);
            this.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            this.Products.Remove(product);
            this.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            this.Categories.Remove(category);
            this.SaveChanges();
        }

        public void displayCategories()
        {
            var query = this.Categories;
            logger.Info(query.Count() + " Catergories returned");

            Console.WriteLine("\nAll Categories in the database:");
            foreach (var item in query)
            {
                Console.WriteLine("{0}) {1}", item.CategoryID, item.CategoryName);
            }
            this.SaveChanges();
        }

        public void displayProducts()
        {
            var query = this.Products;
            logger.Info(query.Count() + " Products returned");

            Console.WriteLine("\nAll Products in the database:");
            Console.Write("All Products in ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("red");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" are out of stock");
            Console.WriteLine();
            foreach (var item in query)
            {
                if(item.UnitsInStock == 0)
                {
                    if(item.Discontinued)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}) {1} ***Discontinued***", item.ProductID, item.ProductName);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}) {1}", item.ProductID, item.ProductName);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    if(item.Discontinued)
                    {
                        Console.WriteLine("{0}) {1} ***Discontinued***", item.ProductID, item.ProductName);
                    }
                    else
                    {
                        Console.WriteLine("{0}) {1}", item.ProductID, item.ProductName);
                    }
                }
            }
            this.SaveChanges();
        }

        public void displayProductsProfit()
        {
            var query = this.Products;
            logger.Info(query.Count() + " Products returned");

            Console.WriteLine("\nAll Products in the database:");

            Console.Write("All Products in ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("red");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" are not very profitable");
            Console.WriteLine();
            Console.Write("All Products in ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("green");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" are very profitable");
            Console.WriteLine("\n");
            foreach (var item in query)
            {
                if(item.UnitPrice-item.WholesalePrice <2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}) {1}: {2}", item.ProductID, item.ProductName,(item.UnitPrice-item.WholesalePrice).ToString("C2"));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if(item.UnitPrice-item.WholesalePrice>8)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0}) {1}: {2}", item.ProductID, item.ProductName,(item.UnitPrice-item.WholesalePrice).ToString("C2"));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("{0}) {1}: {2}", item.ProductID, item.ProductName,(item.UnitPrice-item.WholesalePrice).ToString("C2"));
                }
            }
            this.SaveChanges();
        }

        public void displaySuppliers()
        {
            var query = this.Suppliers;
            logger.Info(query.Count() + " Suppliers returned");

            Console.WriteLine("\nAll Suppliers in the database:");
            foreach (var item in query)
            {
                Console.WriteLine("{0}) {1}", item.SupplierID, item.CompanyName);
            }
            this.SaveChanges();
        }
    }
}
