using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public partial class Form1 : Form
    {

        struct product
        {
            public int _Code;
            public string _Name;
            public int _Prix;
            public int _quantity;

            public product(int code, string name, int prix, int quantity)
            {
                _Code = code;
                _Name = name;
                _Prix = prix;
                _quantity = quantity;
            }
        }


        List<product> Products = new List<product>();

        product Product = new product();
        public Form1()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rn = new Random();
            Product = Products[rn.Next(0, Products.Count - 1)];
            AddProduct(Products, Product);
        }

        private void CalculatePriceAndChangeName(List<product> products, product RandomProduct)
        {
            lblTotalPrice.Text = CalCulateTotalPrice(dataGridView1).ToString();
            lblProductName.Text = RandomProduct._Name;
        }
        private void AddProduct(List<product> products, product RandomProduct)
        {
            if (!isExistInDataGridVieaw(dataGridView1, RandomProduct))
            {
                AddNewItemToDataGridView(dataGridView1, RandomProduct);
                CalculatePriceAndChangeName(products, RandomProduct);
            }
            else
            {
                incimentQuantity(RandomProduct, dataGridView1);
                CalculatePriceAndChangeName(products, RandomProduct);
            }
        }

        private void incimentQuantity(product randomProduct, DataGridView dataGridView1)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;


                if (row.Cells["Code"].Value.ToString() == randomProduct._Code.ToString())
                {
                    row.Cells["Quantity"].Value = (Convert.ToInt32(row.Cells["Quantity"].Value) + 1).ToString();
                }

            }
        }

        DataTable productTable = new DataTable();

        private int CalCulateTotalPrice(DataGridView dataGridView1)
        {
            int totalSum = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;


                if (row.Cells["Prix"].Value != null && row.Cells["Quantity"].Value != null)
                {
                    int price = Convert.ToInt32(row.Cells["Prix"].Value);
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    totalSum += price * quantity;
                }

            }

            return totalSum;
        }

        private void AddNewItemToDataGridView(DataGridView DataGridView, product Product)
        {
            if (DataGridView.ColumnCount == 0)
            {
                DataGridView.Columns.Add("Code", "Code");
                DataGridView.Columns.Add("Name", "Name");
                DataGridView.Columns.Add("Prix", "Prix");
                DataGridView.Columns.Add("Quantity", "Quantity");
            }
            dataGridView1.Rows.Add(Product._Code, Product._Name, Product._Prix, Product._quantity);
        }

        private bool isExistInDataGridVieaw(DataGridView DataGridView, product Product)
        {
            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                if (row.Cells["Code"].Value != null && (int)row.Cells["Code"].Value == Product._Code)
                {
                    return true;
                }
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Code", "Code");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Prix", "Prix");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            Products.Add(new product(101, "Laptop", 1500, 1));
            Products.Add(new product(102, "Mouse", 20, 1));
            Products.Add(new product(103, "Keyboard", 45, 1));
            Products.Add(new product(104, "KAmira", 45, 1));
            Products.Add(new product(105, "MIcro", 45, 1));
            Products.Add(new product(106, "Tilifon", 45, 1));

        }
    }
}
