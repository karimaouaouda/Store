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
        struct Product
        {
            public string Name;
            public int Prix;
            public string Code;
            public int _Contiti;
            public Product(string name, int prix, string code,int Contiti)
            {
                Name = name;
                Prix = prix;
                Code = code;
                _Contiti = Contiti;
            }
        }


        Product[] products =new Product[3];

        List <Product> Products;

        int PrivRandom = 0;
        void AddProductRandomly(Product[] products,int RandomProduct)
        {
            
            if (RandomProduct== PrivRandom)
            {
                products[RandomProduct]._Contiti++;
                dataTable.Clear();
                foreach (Product product in products)
                {
                    dataTable.Rows.Add(product.Name, product.Prix, product.Code, product._Contiti);
                }
                dataGridView1.DataSource = dataTable;
            }
            PrivRandom = RandomProduct;
            Price = Price + products[RandomProduct].Prix;
            lblProductName.Text= products[RandomProduct].Name;  
            label1.Text = Price.ToString();
        }

        public Form1()
        {

            InitializeComponent();
        }

         public DataTable dataTable = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {

            products[0] = new Product("Laptop", 10, "P101",0);
            products[1] = new Product("Smartphone", 20, "P102", 0);
            products[2] = new Product("Headphones", 15, "P103", 0);

            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Prix", typeof(int));
            dataTable.Columns.Add("Code", typeof(string));
            dataTable.Columns.Add("Contiti", typeof(string));

            // Fill the DataTable with data from the array
            foreach (Product product in products)
            {
                dataTable.Rows.Add(product.Name, product.Prix, product.Code,product._Contiti);
            }
            //dataGridView1.Columns[0].HeaderText = "ID";
            //dataGridView1.Columns[1].HeaderText = "Code";
            //dataGridView1.Columns[2].HeaderText = "Name";
            //dataGridView1.Columns[2].HeaderText = "Prix";
            dataGridView1.DataSource = dataTable;
        }

        public int Price = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Random rn = new Random();
            int RanProduct = rn.Next(0, products.Length);
            AddProductRandomly(products, RanProduct);

        }
    }
}
