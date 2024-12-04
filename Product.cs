using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public class Product
    {
        public long  codeBare;
        public string name { get; set; }

        public float price;
        enum enMode { add, update }

        string filePath = @"C:\Users\bille\OneDrive\Desktop\github\Store\Products.txt";
        enMode Mode = enMode.add;
        public Product(int code, string name, int price)
        {
            this.codeBare = code;
            this.name = name;
            this.price = price;
        }
        public Product()
        {

        }
        public long getCode()
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

        public void setCode(long code)
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
            switch (Mode)
            {
                case enMode.add:
                    {
                        if (AddProductToFile(this))
                            return true;
                        else
                            return false;
                    }
                case enMode.update:
                    {
                        if (UpdateProduct(this))
                            return true;
                        else
                            return false;
                    }
                default:
                    return false;
            }
        }


        private bool UpdateProduct(Product product)
        {
            List<Product> products = new List<Product>();
            products = GetAllProducts();
            foreach (Product p in products)
            {
                if (p.codeBare == product.getCode())
                {
                    p.name = product.name;            // Assuming name is a property
                    p.price = product.price;
                }
            }

            return _UpdateProductsInFile(products);
        }

        private bool _UpdateProductsInFile(List<Product> products)
        {
            try
            {
                File.WriteAllText(filePath, string.Empty);

                foreach (Product p in products)
                {
                    string lineToAdd = p.codeBare.ToString() + "#//#" + p.name + "#//#" + p.price.ToString();
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(lineToAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
            return true;
        }
        private bool AddProductToFile(Product product)
        {

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

        public static Product Find(long codeBare)
        {
            List<Product> products = new List<Product>();
            products = GetAllProductsFromTheFile();

            foreach (Product product in products)
            {
                if (product.codeBare == codeBare)
                    return product;
            }
            return null;
        }

        public static bool IsExis(Product product)
        {
            return Find(product.codeBare) != null;
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
                    if (long.TryParse(ProductInfo[0], out long barcodeAsLong))
                    {
                   
                    prod.setCode(barcodeAsLong);
                    }
                    else
                    {
                        MessageBox.Show("Invalid barcode! Could not convert to long.");
                    }
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
