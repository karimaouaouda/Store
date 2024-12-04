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
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product newProduct = new Product();

            if (long.TryParse(txtProdCB.Text, out long barcodeAsLong))
            {


                newProduct.setCode(barcodeAsLong);

            }
            else
            {
                MessageBox.Show("Invalid barcode! Could not convert to long.");
            }
            newProduct.setPrice(Convert.ToSingle(numericUpDownPrix.Value));
            newProduct.name = txtProdName.Text;

            if (Product.IsExis(newProduct))
            {
                MessageBox.Show("The Product is already exist ", "Information");
                return;
            }

            if (newProduct.save())
            {

                MessageBox.Show("The Product Added Succesusfully", "Information");

                frmAddProduct_Load(null, null);
            }
            else
            {
                MessageBox.Show("The Product Does Not Added ", "Information");

            }

        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            dgvListProd.DataSource = ConvertToDataTable(Product.GetAllProducts());
            CustomizeDataGridView(dgvListProd);
        }



        public static DataTable ConvertToDataTable(List<Product> products)
        {
            // Create a new DataTable
            DataTable dt = new DataTable();

            // Define columns in the DataTable
            dt.Columns.Add("Code", typeof(string));  // Assuming 'Code' is a string
            dt.Columns.Add("Name", typeof(string));  // Assuming 'Name' is a string
            dt.Columns.Add("Price", typeof(decimal));  // Assuming 'Price' is a decimal

            // Loop through the List<Product> and add rows to the DataTable
            foreach (var product in products)
            {
                dt.Rows.Add(product.codeBare, product.name, product.price);
            }

            return dt;
        }

        private void updatePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = Product.Find(Convert.ToInt32(dgvListProd.CurrentRow.Cells[0].Value));
            Form UpdateProuct = new frmUpdateProduct(product);
            UpdateProuct.ShowDialog();
            frmAddProduct_Load(null, null);
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

        private void txtProdCB_TextChanged(object sender, EventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {

        }

    }
}
