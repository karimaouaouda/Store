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

        private float totalPrice = 0;

        DataTable ProductTable = new DataTable();

        List<Product> products = new List<Product>();

        BuySession currentBuySession = null;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("Code", "Code");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Prix", "Prix");
            dataGridView1.Columns.Add("Quantity", "Quantity");

            this.currentBuySession = new BuySession(dataGridView1.Rows);
        }

        private void addItemEventHandler(object sender, EventArgs e)
        {
            Product randomProduct = selectRandomProduct();
            AddProduct(randomProduct);
        }

        private Product selectRandomProduct()
        {
            Random rand = new Random();

            return products[rand.Next(0, products.Count - 1)];
        }

        private void CalculatePriceAndChangeName(Product RandomProduct)
        {
            lblTotalPrice.Text = this.totalPrice.ToString();
            lblProductName.Text = RandomProduct.name;
        }
        private void AddProduct(Product randomProduct)
        {
            this.currentBuySession.pushProduct(randomProduct);

            lblTotalPrice.Text = this.currentBuySession.getTotalPrice().ToString();

        }


        private void AddNewItemToDataGridView(Product Product)
        {
            this.currentBuySession.pushProduct(Product);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


           products = Product.GetAllProducts();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frmAddProducts = new frmAddProduct();
            frmAddProducts.ShowDialog();
        }
    }
}
