using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
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
            lblProductName.Text = randomProduct.name;
        }


        private void AddNewItemToDataGridView(Product Product)
        {
            this.currentBuySession.pushProduct(Product);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            products = Product.GetAllProducts();
            CustomizeDataGridView(dataGridView1);
            txtProductCodeBare.TabIndex = 0;
            txtProductCodeBare.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frmAddProducts = new frmAddProduct();
            frmAddProducts.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.currentBuySession.clear();
            lblTotalPrice.Text = 0.ToString();
            Form1_Load(null, null);
        }

        public void CustomizeDataGridView(DataGridView dataGridView1)
        {
            // تخصيص الخطوط
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);

            // تخصيص الألوان
            dataGridView1.BackgroundColor = Color.LightGray;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // تخصيص العناوين
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // تخصيص التحديد
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // إيقاف السطر الفارغ
            dataGridView1.AllowUserToAddRows = false;

            // تعديل حجم الأعمدة ليتناسب مع المحتوى
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // تخصيص الحدود بين الخلايا
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        }

        private void txtProductCodeBare_TextChanged(object sender, EventArgs e)
        {


            if (txtProductCodeBare.Text.Count() == 13)
            {
                if (long.TryParse(txtProductCodeBare.Text, out long barcodeAsLong))
                {




                    
                    Product SelectedProduct = Product.Find(barcodeAsLong);
                    this.currentBuySession.pushProduct(SelectedProduct);
                    lblProductName.Text = SelectedProduct.name;
                    lblTotalPrice.Text = this.currentBuySession.getTotalPrice().ToString();

                    txtProductCodeBare.Text = "";
                    txtProductCodeBare.TabIndex = 0;

                }
                else
                {
                    MessageBox.Show("Invalid barcode! Could not convert to long.");
                }
            }
        }

        private void txtProductCodeBare_Leave(object sender, EventArgs e)
        {
            //implimentation
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
