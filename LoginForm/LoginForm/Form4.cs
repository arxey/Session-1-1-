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
using System.IO;

namespace LoginForm
{
    public partial class Form4 : Form
    {
        static string connectionString = "Integrated Security=SSPI; Persist Security Info=False;Initial Catalog=Trade;Data Source=ISP-11-11\\SQLEXPRESS";
        SqlConnection connection = new SqlConnection(connectionString);
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
