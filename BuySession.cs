using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    internal class BuySession
    {
        private string _sessionId;

        private float _totalAmount;

        private DateTime _lastUpdateTime;

        private DataGridViewRowCollection products = null;

        public BuySession(DataGridViewRowCollection products) {
            this.generateSessionId();
            this.products = products;
        }


        public float getTotalPrice()
        {
            return this._totalAmount;
        }
        public void pushProduct(Product product)
        {
            if( !this.isExists(product))
            {
                this._add(product);
            }
            else
            {
                this.increamentQuantity(product);
               
            }

            this.increamenttotalPrice(product.getPrice());
        }

        private bool isExists(Product product)
        {
            foreach (DataGridViewRow row in this.products)
            {
                if (row.Cells["Code"].Value != null && (int)row.Cells["Code"].Value == product.getCode())
                {
                    return true;
                }
            }
            return false;
        }

        private void increamenttotalPrice(float price)
        {
            this._totalAmount += price;
        }

        private void increamentQuantity(Product product)
        {
            foreach (DataGridViewRow row in this.products)
            {
                if (row.IsNewRow) continue;


                if (row.Cells["Code"].Value.ToString() == product.getCode().ToString())
                {
                    row.Cells["Quantity"].Value = (Convert.ToInt32(row.Cells["Quantity"].Value) + 1).ToString();
                }

            }
        }

        private void _add(Product product)
        {
            this.products.Add(product.getCode(), product.name, product.getPrice(), 1);
        }



        private void generateSessionId()
        {
            this._sessionId = Guid.NewGuid().ToString();
        }
    }
}
