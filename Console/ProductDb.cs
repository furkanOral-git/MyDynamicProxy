using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console
{
    public class ProductDb:IProductDb
    {
        public void AddToDatabase()
        {
            System.Console.WriteLine("Product DataBase'e eklendi...");
        }
    }
}