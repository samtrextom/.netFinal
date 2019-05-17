using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Product
    {
        public int ProductID { get; set; }
        public String ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public String QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public Boolean Discontinued { get; set; }

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Display()
        {
            var db = new TraderContext();
            var catQuery = db.Categories;
            var supQuery = db.Suppliers;

            String Category = "";
            String Supplier = "";

            foreach (var item in catQuery)
            {
                if(item.CategoryID == CategoryID)
                {
                    Category = item.CategoryName;
                }
            }

            foreach (var item in supQuery)
            {
                if (item.SupplierID == SupplierID)
                {
                    Supplier = item.CompanyName;
                }
            }

            Console.WriteLine("\nProduct ID: " + ProductID);
            Console.WriteLine("Product Name: " + ProductName);
            Console.WriteLine("Supplier: " + Supplier);
            Console.WriteLine("Category: " + Category);
            Console.WriteLine("Quantity Per Unit: " + QuantityPerUnit);
            Console.WriteLine("Unit Price: " + UnitPrice);
            Console.WriteLine("Wholesale Price: " + WholesalePrice);
            Console.WriteLine("Units In Stock: " + UnitsInStock);
            Console.WriteLine("Units On Order: " + UnitsOnOrder);
            Console.WriteLine("Reorder Level: " + ReorderLevel);
            if(!Discontinued)
            {
                Console.WriteLine("Item is Active");
            }
            else
            {
                Console.WriteLine("Item is Discontinued");
            }
        }

        public void editProductDetails()
        {
            Boolean inProDetails = true;

            do
            {
                printProductDetailsMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("\nEnter new Quantity per Unit");
                            var newValue = Console.ReadLine();

                            try
                            {
                                this.QuantityPerUnit = newValue;
                            }
                            catch
                            {
                                logger.Info("Not a valid Quantity per Unit - Try Again");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("\nEnter new Unit Price");
                            var newValue = Console.ReadLine();

                            try
                            {
                                this.UnitPrice = decimal.Parse(newValue);
                            }
                            catch
                            {
                                logger.Info("Not a valid Unit Price - Try Again");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\nEnter new Wholesale Price");
                            var newValue = Console.ReadLine();

                            try
                            {
                                this.WholesalePrice = decimal.Parse(newValue);
                            }
                            catch
                            {
                                logger.Info("Not a valid Wholesale Price - Try Again");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\nEnter new Units in Stock");
                            var newValue = Console.ReadLine();

                            try
                            {
                                this.UnitsInStock = short.Parse(newValue);
                            }
                            catch
                            {
                                logger.Info("Not a valid Units in Stock - Try Again");
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("\nEnter new Units on Order");
                            var newValue = Console.ReadLine();

                            try
                            {
                                this.UnitsOnOrder = short.Parse(newValue);
                            }
                            catch
                            {
                                logger.Info("Not a valid Units on Order - Try Again");
                            }
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("\nEnter new Reorder Level");
                            var newValue = Console.ReadLine();

                            try
                            {
                                this.ReorderLevel = short.Parse(newValue);
                            }
                            catch
                            {
                                logger.Info("Not a valid Reorder Level - Try Again");
                            }
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("\nIs Product Discontinued? (y/n)");
                            var newValue = Console.ReadLine();

                            if (newValue.ToUpper().Equals("Y"))
                            {
                                this.Discontinued = true;
                            }
                            else if (newValue.ToUpper().Equals("N"))
                            {
                                this.Discontinued = false;
                            }
                            else
                            {
                                logger.Info("Did not enter 'y' or 'n' - Try Again");
                            }
                            break;
                        }
                    case "8":
                        {
                            Console.WriteLine("\nAre you sure? (y/n)");

                            if (Console.ReadLine().ToUpper().Equals("Y"))
                            {
                                inProDetails = false;
                            }
                            break;
                        }
                    default:
                        {
                            logger.Info("Not a valid menu option");
                            break;
                        }
                }
            } while (inProDetails);
        }

        public static void printProductDetailsMenu()
        {
            Console.WriteLine("\n**Product Details**");
            Console.WriteLine("1) Edit Quantity Per Unit");
            Console.WriteLine("2) Edit Unit Price");
            Console.WriteLine("3) Edit Wholesale Price");
            Console.WriteLine("4) Edit Units In Stock");
            Console.WriteLine("5) Edit Units On Order");
            Console.WriteLine("6) Edit Reorder Level");
            Console.WriteLine("7) Edit Discontinued");
            Console.WriteLine("8) Return to Main Menu");
        }

    }

    
}
