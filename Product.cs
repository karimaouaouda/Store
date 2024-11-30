using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    internal class Product
    {
        private int code;
        public string name;
        private float price;
        

        public Product(int code, string name, int price)
        {
            this.code = code;
            this.name = name;
            this.price = price;
        }

        public int getCode()
        {
            return code;
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
            /// Check the pattern of the code
            /// </remarks>

            this.code = code;
        }


        public bool isSameCodeAs(Product other)
        {
            return this.code == other.getCode();
        }

    }
}
