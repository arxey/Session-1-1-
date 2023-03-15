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
    public partial class Goods : Form
    {
        static string connectionString = "Integrated Security=SSPI; Persist Security Info=False;Initial Catalog=Trade;Data Source=DESKTOP-L9GHSSP\\SQLEXPRESS";
        SqlConnection connection = new SqlConnection(connectionString);
        Color col = ColorTranslator.FromHtml("#7fff00");
        string selectedArticle;
      
        public Goods()
        {
            InitializeComponent();
        }

        private void Goods_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ProductArticleNumber as Артикул, ProductName as Наименование, ProductDescription as Описание, ProductCategory as Категория, ProductPhoto as Изображение, ProductManufacturer as Производитель, ProductCost as Стоимость, ProductDiscountAmount as Сумма_скидки, ProductQuantityInStock as Количество_на_складе, ProductStatus as Статус FROM Product";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            
            GoodsView.DataSource = dataset.Tables[0];
          
            GoodsView.Columns[0].Visible = false;
            label9.Text = (GoodsView.Rows.Count - 1).ToString();
            change_count();
            connection.Close();
        }

        private void GoodsView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (GoodsView.Rows[e.RowIndex].Cells[0].Value != DBNull.Value)
            {
                int value = Convert.ToInt32(GoodsView.Rows[e.RowIndex].Cells[7].Value);

             if (value >= 15 )
                {
                    GoodsView.Rows[e.RowIndex].DefaultCellStyle.BackColor = col;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (GoodsView.DataSource as DataTable).DefaultView.RowFilter = $"Наименование LIKE '%{textBox1.Text}%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (GoodsView.DataSource as DataTable).DefaultView.RowFilter = "";
                    break;
                case 1:
                    (GoodsView.DataSource as DataTable).DefaultView.RowFilter = $"Сумма_скидки <= 5";
                    break;
                case 2:
                    (GoodsView.DataSource as DataTable).DefaultView.RowFilter = $"Сумма_скидки > 5 and Сумма_скидки <= 15";
                    break;
                case 3:
                    (GoodsView.DataSource as DataTable).DefaultView.RowFilter = $"Сумма_скидки > 15";
                    break;
            }

            change_count();
        }

        public void change_count()
        {
            labelCount.Text = (GoodsView.Rows.Count - 1).ToString();
        }

        private void GoodsView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label4.Text = GoodsView.CurrentRow.Cells[7].Value.ToString();
            label5.Text = GoodsView.CurrentRow.Cells[1].Value.ToString();
            label6.Text = GoodsView.CurrentRow.Cells[2].Value.ToString();
            label10.Text = GoodsView.CurrentRow.Cells[5].Value.ToString();
            label11.Text = GoodsView.CurrentRow.Cells[6].Value.ToString();
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FormAddProduct"] == null)
            {
                FormAddProduct fm = new FormAddProduct();
                this.Hide();
                fm.ShowDialog();
                this.Show();
            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void GoodsView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = GoodsView.HitTest(e.X, e.Y);
                if (hit.RowIndex >=0)
                {
                    GoodsView.ClearSelection();
                    GoodsView.Rows[hit.RowIndex].Selected = true;
                    contextMenuStrip1.Show(GoodsView, e.Location);
                    selectedArticle = GoodsView.SelectedRows[0].Cells[0].Value.ToString();
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
          
            Form Form3 = new FormAddProduct(selectedArticle);
            Hide();
            Form3.ShowDialog();
            this.Show();
           
        }

        private void Goods_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Goods_Shown(object sender, EventArgs e)
        {
          
        }

        private void Goods_Activated(object sender, EventArgs e)
        {
       
        }

        private void Goods_Paint(object sender, PaintEventArgs e)
        {
            Goods_Load(sender, e);
        }
    }
}
