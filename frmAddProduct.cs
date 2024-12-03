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
            Product newProduct=new Product();
            newProduct.setCode(Convert.ToInt32(txtProdCB.Text));
            newProduct.setPrice(Convert.ToSingle(numericUpDownPrix.Value));
            newProduct.name=txtProdName.Text;

            if(newProduct.save())
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
    }
}
