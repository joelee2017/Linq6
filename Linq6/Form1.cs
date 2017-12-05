using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using Linq6.Properties;

namespace Linq6
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable dtProduct;

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLcalDB;" +
                 "AttachDbFilename=|DataDirectory|Northwind.mdf;" +
                 "Integrated Security=True";

            SqlDataAdapter da = new SqlDataAdapter
                     ("select 產品編號,產品,單價,庫存量 from", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dtProduct = ds.Tables[0];
            dataGridView1.DataSource = dtProduct;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = from r in dtProduct.AsEnumerable()
                         select new {
                             單價 = r.Field<decimal>("單價")
                                    };

            string str = "資料筆數：" + result.Count() +
                "\n 最高單價：" + result.Max(r => r.單價) +
                "\n 最低單價：" + result.Min(r => r.單價) +
                "\n 平均單價：" + (int)result.Average(r => r.單價) +
                "\n 單價總和：" + result.Sum(r => r.單價);
            MessageBox.Show(str);
        }
    }
}
