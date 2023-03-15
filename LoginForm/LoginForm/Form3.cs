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
    public partial class FormAddProduct : Form
    {
        static string connectionString = "Integrated Security=SSPI; Persist Security Info=False;Initial Catalog=Trade;Data Source=DESKTOP-L9GHSSP\\SQLEXPRESS";
        SqlConnection connection = new SqlConnection(connectionString);
        string ProductArticle;
        bool AddRegime;
        public FormAddProduct()
        {
            AddRegime = true;
            InitializeComponent();
        }

        public FormAddProduct(string article)
        {

            ProductArticle = article;
            AddRegime = false;
            InitializeComponent();
        }

        private void FormAddProduct_Load(object sender, EventArgs e)
        {
            if (AddRegime)
                button1.Text = "Добавить";
            else
            {
                button1.Text = "Изменить";
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ProductInfo";
                command.Parameters.AddWithValue("@article", ProductArticle);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        textBox1.Text = reader["ProductName"].ToString();
                        richTextBox1.Text = reader["ProductDescription"].ToString();
                        textBox3.Text = reader["ProductCategory"].ToString();
                        textBox4.Text = reader["ProductManufacturer"].ToString();
                        textBox5.Text = reader["ProductCost"].ToString();
                        textBox6.Text = reader["ProductDiscountAmount"].ToString();
                        textBox7.Text = reader["ProductQuantityInStock"].ToString();
                        textBox8.Text = reader["ProductStatus"].ToString();
                        textBox9.Text = reader["ProductArticleNumber"].ToString();
                    }
                }
            }
                
            
          connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            if (AddRegime)
            {
                command.CommandText = "AddProduct";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name1", textBox1.Text);
                command.Parameters.AddWithValue("@description1", richTextBox1.Text);
                command.Parameters.AddWithValue("@category", textBox3.Text);
                command.Parameters.AddWithValue("@manufacturer", textBox4.Text);
                command.Parameters.AddWithValue("@price", Convert.ToInt32(textBox5.Text));
                command.Parameters.AddWithValue("@discount_amount", Convert.ToInt32(textBox6.Text));
                command.Parameters.AddWithValue("@quantity_in_sklad", Convert.ToInt32(textBox7.Text));
                command.Parameters.AddWithValue("@status", textBox8.Text);
                command.Parameters.AddWithValue("@article", textBox9.Text);
            
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                command.CommandText = "UpdateProduct";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name1", textBox1.Text);
                command.Parameters.AddWithValue("@description1", richTextBox1.Text);
                command.Parameters.AddWithValue("@category", textBox3.Text);
                command.Parameters.AddWithValue("@manufacturer", textBox4.Text);
                command.Parameters.AddWithValue("@price", Convert.ToDecimal(textBox5.Text));
                command.Parameters.AddWithValue("@discount_amount", Convert.ToInt32(textBox6.Text));
                command.Parameters.AddWithValue("@quantity_in_sklad", Convert.ToInt32(textBox7.Text));
                command.Parameters.AddWithValue("@status", textBox8.Text);
                command.Parameters.AddWithValue("@article", textBox9.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void FormAddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void FormAddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == '-') || (e.KeyChar == ',') || (e.KeyChar == (char)Keys.Back))
                {
                    return;
                }
                e.Handled = true;
            }
        }
    }
}
