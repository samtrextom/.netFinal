using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Printer
    {
        public void printMainMenu()
        {

            Console.WriteLine("\n****North Winds Traders****");
            Console.WriteLine("1) Edit Products");
            Console.WriteLine("2) Edit Categories");
            Console.WriteLine("3) Exit Program");
        }

        public void printProductsMenu()
        {

            Console.WriteLine("\n***Products***");
            Console.WriteLine("1) Add new Product");
            Console.WriteLine("2) Edit Product");
            Console.WriteLine("3) Display all Products");
            Console.WriteLine("4) Display all Product's by profit");
            Console.WriteLine("5) Display specific Product detail");
            Console.WriteLine("6) Delete Product");
            Console.WriteLine("7) Return to Main Menu");
        }

        public void printCategoriesMenu()
        {

            Console.WriteLine("\n***Categories***");
            Console.WriteLine("1) Add new Category");
            Console.WriteLine("2) Edit Category");
            Console.WriteLine("3) Display all Categories");
            Console.WriteLine("4) Display specific Category detail");
            Console.WriteLine("5) Delete Category");
            Console.WriteLine("6) Return to Main Menu");
        }

    }
}
