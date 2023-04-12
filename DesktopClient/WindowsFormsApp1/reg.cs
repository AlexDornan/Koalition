using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;
using WindowsFormsApp1.Users;


namespace WindowsFormsApp1
{
    public partial class reg : Form
    {
        public reg()
        {
            InitializeComponent();
            clearTexBox();
        }


        private void clearTexBox()
        {
            textBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
        }   

        private async void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text)
                || string.IsNullOrWhiteSpace(textBox4.Text))
            { MessageBox.Show("Заповніть поля!"); }
            else
            {
                if(textBox3.Text != textBox5.Text){
                    MessageBox.Show("Пароль не збігається!");
                }
                else
                {
                    HttpClient httpClient = new HttpClient();
                    User users = new User();
                    users.login = textBox4.Text.ToString();
                    users.name = textBox1.Text.ToString();
                    users.email = textBox2.Text.ToString();
                    users.password = textBox3.Text.ToString();
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(users);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7127/api/Users/register", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Ви успішно зареєструвалися!");
                        clearTexBox();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Сталася помилка!");
                    }
                }
            }         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
