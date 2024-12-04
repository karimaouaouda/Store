using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public partial class frmUpdateProduct : Form
    {

        private Product _Product = null;
        public frmUpdateProduct(Product product)
        {
            InitializeComponent();
            _Product = product;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Product.setPrice(Convert.ToSingle(numericUpDownPrix.Value));
            _Product.name = txtProdName.Text;

            if(_Product.save())
            {
                MessageBox.Show("yes");
            }
            else
            {
                MessageBox.Show("no");
            }
                
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LoadProductInfo()
        {
            txtProdCB.Text = _Product.codeBare.ToString();
            txtProdName.Text = _Product.name;
            numericUpDownPrix.Value = Convert.ToInt32(_Product.price);
        }
        private void frmUpdateProduct_Load(object sender, EventArgs e)
        {
            LoadProductInfo();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
