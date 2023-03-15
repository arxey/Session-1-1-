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

    public partial class SignForm : Form
    {
        static string connectionString = "Integrated Security=SSPI; Persist Security Info=False;Initial Catalog=Trade;Data Source=DESKTOP-L9GHSSP\\SQLEXPRESS";
        private string text = String.Empty;
        SqlConnection connection = new SqlConnection(connectionString);
        int count = 0;
        int second = 0;
        public SignForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void checkShowPassword_CheckedChanged(object sender, EventArgs e) //функция показать или скрыть пароль
        {
            if (checkShowPassword.Checked)
            {
                PasswordText.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordText.UseSystemPasswordChar = true;
            }
        }
        private void checkRememberMe_CheckedChanged(object sender, EventArgs e) //функция запомнить меня
        {
            using (StreamWriter wr = new StreamWriter("Password.txt"))
            {
                wr.WriteLine(LoginText.Text);
                wr.WriteLine(PasswordText.Text);
                wr.Close();
            }
            if (checkRememberMe.Checked)
            {

            }
            else
            {
                File.WriteAllText("Password.txt", "");
            }
            using (StreamReader reader = new StreamReader("Password.txt"))
            {
                while (!reader.EndOfStream)
                {
                    LoginText.AppendText(reader.ReadLine());
                    PasswordText.AppendText(reader.ReadLine());
                }
            }
        }
        private void ButtonSign_Click(object sender, EventArgs e)
        {

            if (connection.State == ConnectionState.Open)
                connection.Close();

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Sign1";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@login", LoginText.Text);
            command.Parameters.AddWithValue("@password", PasswordText.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                {
                    reader["UserLogin"].ToString();
                    reader["UserPassword"].ToString();
                }
                switch (reader.GetInt32(2))
                {
                    case 1:
                        Form Form2 = new Goods();
                        Hide();
                        Form2.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
                captchaPb.Image = this.CreateImage(captchaPb.Width, captchaPb.Height);
                count = 0;
                textBox1.Visible = true;
            }
            if (reader.HasRows)
            {
                reader.Read();
                count++;
                if (count == 1)
                {
                    //timerFailSign.Start();
                    //ButtonSign.Enabled = false;
                    //count = 0;
                    captchaPb.Image = this.CreateImage(captchaPb.Width, captchaPb.Height);
                    count = 0;
                    textBox1.Visible = true;
                }
            }

            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
            private void timerFailSign_Tick(object sender, EventArgs e)
            {
                labelWait.Text = string.Format($"Пожалуйста подождите {second} секунд", second--);
                labelWait.Visible = true;
                if (second == -1)
                {
                    timerFailSign.Stop();
                    ButtonSign.Enabled = true;
                    labelWait.Visible = false;
                    second = 10;
                }
            }
            private Bitmap CreateImage(int Width, int Height)
            {
                Random rnd = new Random();

                //Создадим изображение
                Bitmap result = new Bitmap(Width, Height);

                //Вычислим позицию текста
                int Xpos = rnd.Next(0, 30);
                int Ypos = rnd.Next(0, 30);

                //Добавим различные цвета
                Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

                //Укажем где рисовать
                Graphics g = Graphics.FromImage((Image)result);

                //Пусть фон картинки будет серым
                g.Clear(Color.Gray);

                //Сгенерируем текст
                Text = String.Empty;
                string ALF = "hudsisoidvjisjiosdsi";
                for (int i = 0; i < 5; ++i)
                    Text += ALF[rnd.Next(ALF.Length)];

                //Нарисуем сгенерируемый текст
                g.DrawString(Text,
                    new Font("Arial", 15),
                    colors[rnd.Next(colors.Length)],
                    new PointF(Xpos, Ypos));

                //Добавим немного помех
                /////Линии из углов
                g.DrawLine(Pens.Black,
                    new Point(rnd.Next(0, 30), rnd.Next(0, 30)),
                    new Point(rnd.Next(30, 60), rnd.Next(30, 60)));
                g.DrawLine(Pens.Black,
                    new Point(rnd.Next(0, 30), rnd.Next(0, 30)),
                    new Point(rnd.Next(30, 60), rnd.Next(30, 60)));
                ////Белые точки
                for (int i = 0; i < Width; ++i)
                    for (int j = 0; j < Height; ++j)
                        if (rnd.Next() % 20 == 0)
                            result.SetPixel(i, j, Color.White);
                return result;
            }

        private void SignForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }

