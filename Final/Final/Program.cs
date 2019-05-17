using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Final
{

    class Program
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            Boolean inProgram = true;
            Printer printer = new Printer();
            logger.Info("Program started");

            do
            {
                Console.WriteLine("****Welcome to****");
                printer.printMainMenu();
                String choice = Console.ReadLine();

                logger.Info("User choice - {choice}", choice);

                switch (choice)
                {
                    /////////////////////////
                    //PRODUCTS MENU//////////
                    /////////////////////////
                    case "1":
                        {

                            Boolean inProducts = true;
                            do
                            {

                                printer.printProductsMenu();
                                choice = Console.ReadLine();

                                logger.Info("User choice - {choice}", choice);

                                switch (choice)
                                {
                                    /////////////////////////
                                    //ADD PRODUCT////////////
                                    /////////////////////////
                                    case "1":
                                        {
                                            Boolean productIsValid = false;
                                            var db = new TraderContext();
                                            var proQuery = db.Products.OrderBy(b => b.ProductName);
                                            var catQuery = db.Categories;
                                            var supQuery = db.Suppliers;

                                            //verify that the new Product is a new item and not null
                                            do
                                            {
                                                var validName = true;

                                                //new Product values
                                                String proName;
                                                int catID;
                                                int supID;

                                                Console.WriteLine("\nEnter the name of the new Product");
                                                proName = Console.ReadLine();

                                                foreach (var c in proQuery)
                                                {
                                                    if (c.ProductName.ToUpper().Equals(proName))
                                                    {
                                                        validName = false;
                                                    }
                                                }

                                                if (proName is null)
                                                {
                                                    logger.Info("Name cannot be null - Try Again");
                                                }
                                                else if (!validName)
                                                {
                                                    logger.Info("Product {name} already exsists", proName);
                                                }
                                                else
                                                {
                                                    productIsValid = true;
                                                    Boolean catIDValid = false;
                                                    Boolean supIDValid = false;

                                                    //verify CategoryID
                                                    do
                                                    {
                                                        Console.WriteLine("\nPlease choose a Category for the Product");
                                                        db.displayCategories();
                                                        var input = Console.ReadLine();

                                                        if (int.TryParse(input, out catID))
                                                        {
                                                            foreach (var item in catQuery)
                                                            {
                                                                if (item.CategoryID == catID)
                                                                {
                                                                    catIDValid = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            logger.Info("Not a valid Category ID - Try Again");
                                                        }
                                                    } while (!catIDValid);

                                                    //verify SupplierID
                                                    do
                                                    {
                                                        Console.WriteLine("\nPlease choose a Supplier for the Product");
                                                        db.displaySuppliers();
                                                        var input = Console.ReadLine();

                                                        if (int.TryParse(input, out supID))
                                                        {
                                                            foreach (var item in supQuery)
                                                            {
                                                                if (item.SupplierID == supID)
                                                                {
                                                                    supIDValid = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            logger.Info("Not a valid Supplier ID - Try Again");
                                                        }
                                                    } while (!supIDValid);

                                                    var product = new Product
                                                    {
                                                        ProductName = proName,
                                                        CategoryID = catID,
                                                        SupplierID = supID,
                                                        Discontinued = false
                                                    };
                                                    db.AddProduct(product);
                                                    logger.Info("Product added - {name}", proName);

                                                    Console.WriteLine("\nWould you like to enter details for the new Product? (y/n)");

                                                    if (Console.ReadLine().ToUpper().Equals("Y"))
                                                    {
                                                        var updateProduct = db.Products.SingleOrDefault(p => p.ProductName == proName);
                                                        updateProduct.editProductDetails();
                                                    }
                                                }
                                            } while (!productIsValid);
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //EDIT PRODUCT///////////
                                    /////////////////////////
                                    case "2":
                                        {
                                            var db = new TraderContext();
                                            var query = db.Products;
                                            Boolean isValidProductID = false;
                                            db.displayProducts();
                                            Console.WriteLine("\nChoose a Product to edit");
                                            var input = Console.ReadLine();
                                            var proID = 0;
                                            if (int.TryParse(input, out proID))
                                            {


                                                foreach (var item in query)
                                                {
                                                    if (item.ProductID == proID)
                                                    {
                                                        isValidProductID = true;
                                                    }
                                                }

                                                if (isValidProductID)
                                                {
                                                    var updateProduct = db.Products.SingleOrDefault(p => p.ProductID == proID);
                                                    updateProduct.editProductDetails();
                                                }
                                                else
                                                {
                                                    logger.Info("Not a valid Product ID");
                                                }

                                            }
                                            else
                                            {
                                                logger.Info("Not a valid Integer");
                                            }
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //DISPLAY PRODUCTS///////
                                    /////////////////////////
                                    case "3":
                                        {
                                            var db = new TraderContext();
                                            db.displayProducts();
                                            db.SaveChanges();
                                            break;
                                        }
                                    case "4":
                                        {
                                            var db = new TraderContext();
                                            db.displayProductsProfit();
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //VIEW PRODUCT DETAIL////
                                    /////////////////////////
                                    case "5":
                                        {
                                            var db = new TraderContext();
                                            var query = db.Products;
                                            Boolean isValidProductID = false;
                                            db.displayProducts();
                                            Console.WriteLine("\nChoose a Product detail you want to select");
                                            var input = Console.ReadLine();
                                            int proID = 0;
                                            if (int.TryParse(input,out proID))
                                            {
                                                

                                                foreach (var item in query)
                                                {
                                                    if (item.ProductID == proID)
                                                    {
                                                        isValidProductID = true;
                                                    }
                                                }

                                                if (isValidProductID)
                                                {
                                                    var displayProduct = db.Products.SingleOrDefault(p => p.ProductID == proID);
                                                    displayProduct.Display();
                                                }
                                                else
                                                {
                                                    logger.Info("Not a valid Product ID");
                                                }

                                            }
                                            else
                                            {
                                                logger.Info("Not a valid Integer");
                                            }
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //DELETE PRODUCT/////////
                                    /////////////////////////
                                    case "6":
                                        {

                                            var db = new TraderContext();
                                            var query = db.Products;
                                            Boolean isValidProductID = false;
                                            db.displayProducts();
                                            Console.WriteLine("\nChoose a Product you would like to delete");
                                            var input = Console.ReadLine();
                                            int proID = 0;
                                            if (int.TryParse(input, out proID))
                                            {
                                                foreach (var item in query)
                                                {
                                                    if (item.ProductID == proID)
                                                    {
                                                        isValidProductID = true;
                                                    }
                                                }

                                                if (isValidProductID)
                                                {
                                                    var deleteProduct = db.Products.SingleOrDefault(p => p.ProductID == proID);
                                                    Console.WriteLine("\nAre you sure you want to delete " + deleteProduct.ProductName+ "? (y/n)");

                                                    if (Console.ReadLine().ToUpper().Equals("Y"))
                                                    {
                                                        db.DeleteProduct(deleteProduct);
                                                    }
                                                }
                                                else
                                                {
                                                    logger.Info("Not a valid Product ID");
                                                }
                                            }
                                            else
                                            {
                                                logger.Info("Not a valid Integer");
                                            }
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //RETURN TO MAIN MENU////
                                    /////////////////////////
                                    case "7":
                                        {
                                            Console.WriteLine("\nAre you sure? (y/n)");

                                            if (Console.ReadLine().ToUpper().Equals("Y"))
                                            {
                                                inProducts = false;
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            logger.Info("Not a valid menu option");
                                            break;
                                        }
                                }

                            } while (inProducts);
                            break;
                        }
                    /////////////////////////
                    //CATEGORIES MENU////////
                    /////////////////////////
                    case "2":
                        {

                            Boolean inCategories = true;

                            do
                            {
                                printer.printCategoriesMenu();
                                choice = Console.ReadLine();

                                logger.Info("User choice - {choice}", choice);

                                switch (choice)
                                {
                                    /////////////////////////
                                    //ADD CATEGORY///////////
                                    /////////////////////////
                                    case "1":
                                        {
                                            Boolean categoryIsValid = false;
                                            var db = new TraderContext();
                                            var query = db.Categories.OrderBy(b => b.CategoryName);

                                            do
                                            {
                                                var validName = true;
                                                Console.WriteLine("\nEnter the name of the new Category");
                                                String catName = Console.ReadLine();

                                                foreach (var c in query)
                                                {
                                                    if (c.CategoryName.ToUpper().Equals(catName))
                                                    {
                                                        validName = false;
                                                    }
                                                }

                                                if (catName is null)
                                                {
                                                    logger.Info("Name cannot be null - Try Again");
                                                }
                                                else if (!validName)
                                                {
                                                    logger.Info("Category {name} already exsists", catName);
                                                }
                                                else
                                                {
                                                    String catDescription = "";
                                                    categoryIsValid = true;
                                                    Console.WriteLine("\nWould you like to enter a description for the category? (y/n)");

                                                    if (Console.ReadLine().ToUpper().Equals("Y"))
                                                    {
                                                        Console.WriteLine("\nEnter the description");
                                                        catDescription = Console.ReadLine();
                                                    }

                                                    var category = new Category { CategoryName = catName, Description = catDescription };

                                                    db.AddCategory(category);
                                                    logger.Info("Category added - {name}", catName);
                                                }
                                            } while (!categoryIsValid);
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //EDIT CATEGORY//////////
                                    /////////////////////////
                                    case "2":
                                        {
                                            var db = new TraderContext();
                                            var query = db.Categories;
                                            Boolean isValidCategoryID = false;
                                            db.displayCategories();
                                            Console.WriteLine("\nChoose a Category to edit");
                                            var input = Console.ReadLine();
                                            var catID = 0;
                                            if (int.TryParse(input, out catID))
                                            {


                                                foreach (var item in query)
                                                {
                                                    if (item.CategoryID == catID)
                                                    {
                                                        isValidCategoryID = true;
                                                    }
                                                }

                                                if (isValidCategoryID)
                                                {
                                                    var updateCategory = db.Categories.SingleOrDefault(p => p.CategoryID == catID);
                                                    updateCategory.EditDescription();
                                                }
                                                else
                                                {
                                                    logger.Info("Not a valid Category ID");
                                                }

                                            }
                                            else
                                            {
                                                logger.Info("Not a valid Integer");
                                            }
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //DISPLAY CATEGORIES/////
                                    /////////////////////////
                                    case "3":
                                        {
                                            var db = new TraderContext();
                                            db.displayCategories();
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //VIEW CATEGORY DETAIL///
                                    /////////////////////////
                                    case "4":
                                        {
                                            var db = new TraderContext();
                                            var query = db.Categories;
                                            Boolean isValidCategoryID = false;
                                            db.displayCategories();
                                            Console.WriteLine("\nChoose a Category description you want to view");
                                            var input = Console.ReadLine();
                                            int catID = 0;
                                            if (int.TryParse(input, out catID))
                                            {


                                                foreach (var item in query)
                                                {
                                                    if (item.CategoryID == catID)
                                                    {
                                                        isValidCategoryID = true;
                                                    }
                                                }

                                                if (isValidCategoryID)
                                                {
                                                    var displayCategory = db.Categories.SingleOrDefault(p => p.CategoryID == catID);
                                                    displayCategory.Display();
                                                }
                                                else
                                                {
                                                    logger.Info("Not a valid CategoryID");
                                                }

                                            }
                                            else
                                            {
                                                logger.Info("Not a valid Integer");
                                            }
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //DELETE CATEGORY////////
                                    /////////////////////////
                                    case "5":
                                        {
                                            var db = new TraderContext();
                                            var query = db.Categories;
                                            Boolean isValidCategoryID = false;
                                            db.displayCategories();
                                            Console.WriteLine("\nChoose a Category you wish to delete");
                                            var input = Console.ReadLine();
                                            int catID = 0;
                                            if (int.TryParse(input, out catID))
                                            {


                                                foreach (var item in query)
                                                {
                                                    if (item.CategoryID == catID)
                                                    {
                                                        isValidCategoryID = true;
                                                    }
                                                }

                                                if (isValidCategoryID)
                                                {
                                                    var deleteCategory = db.Categories.SingleOrDefault(p => p.CategoryID == catID);
                                                    Console.WriteLine("Are you sure you want to delete " + deleteCategory.CategoryName + "?(y/n)");

                                                    if (Console.ReadLine().ToUpper().Equals("Y"))
                                                    {
                                                        Console.WriteLine("\nIf you delete " + deleteCategory.CategoryName + " you will delete all Products in this category");
                                                        Console.WriteLine("Would you like to:");
                                                        Console.WriteLine("1) Delete all Products within " + deleteCategory.CategoryName + "?");
                                                        Console.WriteLine("2) Change the Category of all Products within " + deleteCategory.CategoryName + "?");
                                                        var deleteChoice = Console.ReadLine();
                                                        switch(deleteChoice)
                                                        {
                                                            case "1":
                                                                {
                                                                    var deleteProducts = db.Products.Where(p => p.CategoryID == catID);
                                                                    
                                                                    List<Product> productArray = new List<Product>();

                                                                    foreach (var p in deleteProducts)
                                                                    {
                                                                        productArray.Add(p);

                                                                    }

                                                                    foreach (var p in productArray)
                                                                    {
                                                                        db.DeleteProduct(p);
                                                                    }

                                                                    db.DeleteCategory(deleteCategory);
                                                                    break;
                                                                }
                                                            case "2":
                                                                {
                                                                    db.displayCategories();
                                                                    Console.WriteLine("\nChoose wish category you wish to replace "+deleteCategory.CategoryName+" with");
                                                                    String catIDchoice = Console.ReadLine();
                                                                    int newCatID = 0;
                                                                    if (int.TryParse(catIDchoice, out newCatID))
                                                                    {
                                                                        foreach (var item in query)
                                                                        {
                                                                            if (item.CategoryID == newCatID && newCatID!=catID)
                                                                            {
                                                                                isValidCategoryID = true;
                                                                            }
                                                                        }

                                                                        if (isValidCategoryID)
                                                                        {
                                                                            var changeProducts = db.Products.Where(p => p.CategoryID == catID);
                                                                    
                                                                            List<Product> changeProductArray = new List<Product>();

                                                                            foreach (var p in changeProducts)
                                                                            {
                                                                                p.CategoryID = newCatID;
                                                                            }
                                                                            db.DeleteCategory(deleteCategory);
                                                                        }
                                                                        else
                                                                        {
                                                                            logger.Info("Not a valid CategoryID");
                                                                        }
                                                                     }
                                                                    else
                                                                    {
                                                                        logger.Info("Not a valid Integer");
                                                                    }
                                                                    break;
                                                                }
                                                            default:
                                                                {
                                                                    break;
                                                                }
                                                        }
                                                        
                                                    }
                                                }
                                                else
                                                {
                                                    logger.Info("Not a valid CategoryID");
                                                }

                                            }
                                            else
                                            {
                                                logger.Info("Not a valid Integer");
                                            }
                                            db.SaveChanges();
                                            break;
                                        }
                                    /////////////////////////
                                    //RETURN TO MAIN MENU////
                                    /////////////////////////
                                    case "6":
                                        {
                                            Console.WriteLine("\nAre you sure? (y/n)");

                                            if (Console.ReadLine().ToUpper().Equals("Y"))
                                            {
                                                inCategories = false;
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            logger.Info("Not a valid menu option");
                                            break;
                                        }
                                }

                            } while (inCategories);
                            break;
                        }
                    /////////////////////////
                    //EXIT PROGRAM///////////
                    /////////////////////////
                    case "3":
                        {

                            Console.WriteLine("\nAre you sure? (y/n)");

                            if (Console.ReadLine().ToUpper().Equals("Y"))
                            {
                                inProgram = false;
                            }

                            break;
                        }
                    default:
                        {

                            logger.Info("Not a valid menu option");
                            break;
                        }
                }
            } while (inProgram);
            logger.Info("Program ended");
        }
    }
}
