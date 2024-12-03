using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public class Product
    {
        public int codeBare;
        public string name { get; set; }

        public float price;


        public Product(int code, string name, int price)
        {
            this.codeBare = code;
            this.name = name;
            this.price = price;
        }
        public Product()
        {

        }
        public int getCode()
        {
            return codeBare;
        }

        public float getPrice()
        {
            return price;
        }

        public void setPrice(float price)
        {
            if (price < 0)
                throw new Exception("the price must be positive float bumber");

            this.price = price;
        }

        public void setCode(int code)
        {
            /// <remarks>
            /// Check the pattern of the codeBare
            /// </remarks>

            this.codeBare = code;
        }


        public bool isSameCodeAs(Product other)
        {
            return this.codeBare == other.getCode();
        }

        public bool save()
        {
            if (AddProductToFile(this))
                return true;
            else
                return false;
        }

        private bool AddProductToFile(Product product)
        {
            string filePath = @"C:\Users\bille\OneDrive\Desktop\github\Store\Products.txt";
            try
            {
                // Use StreamWriter with append mode
                string lineToAdd = product.codeBare.ToString() + "#//#" + product.name + "#//#" + product.price.ToString();
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(lineToAdd);
                }

                // Confirm the line was added by reading the file
                string[] lines = File.ReadAllLines(filePath);
                return Array.Exists(lines, line => line == lineToAdd); // Check if the line exists
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false in case of an error
            }
        }

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            products = GetAllProductsFromTheFile();


            return products;
        }

        private static List<Product> GetAllProductsFromTheFile()
        {

            List<Product> products = new List<Product>();
            string filePath = @"C:\Users\bille\OneDrive\Desktop\github\Store\Products.txt";

            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    Product prod = new Product();
                    string[] ProductInfo = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    prod.setCode(Convert.ToInt32(ProductInfo[0]));
                    prod.name = Convert.ToString(ProductInfo[1]);
                    prod.price = Convert.ToSingle(ProductInfo[2]);
                    products.Add(prod);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return products;
        }
    }
}
