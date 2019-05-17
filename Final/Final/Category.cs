using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Category
    {
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }
        public String Description { get; set; }

        public void Display()
        {
            Console.WriteLine("\nCategory ID: " + CategoryID);
            Console.WriteLine("Category Name: " + CategoryName);
            if(Description != null)
            {
                Console.WriteLine("Description: " + Description);
            }
            else
            {
                Console.WriteLine("There is no description for this Category");
            }
        }

        public void EditDescription()
        {
            Console.WriteLine("\nAdd the new description for " + CategoryName + " :");
            this.Description = Console.ReadLine();
        }
    }
}
