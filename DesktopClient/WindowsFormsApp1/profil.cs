using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class profil : Form
    {
        LoginResponse loginResponse;
        Form logForm;
        string pass;
        public profil()
        {
            InitializeComponent();
        }

        public profil(LoginResponse loginResponse, Form logForm, string pass)
        {
            InitializeComponent();
            this.loginResponse = loginResponse;
            this.logForm = logForm;
            textBox1.Text = loginResponse.userDetails.name.ToString();
            textBox2.Text = loginResponse.userDetails.email.ToString();
            textBox4.Text = loginResponse.userDetails.login.ToString();
            this.Text = loginResponse.userDetails.login.ToString();
            textBox3.Text = "";
            this.pass = pass;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != pass || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Пароль не співпав!");
                textBox3.Text = "";
            }
            else
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                var response = await client.DeleteAsync("https://localhost:7127/api/Users/currentuser");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ви успішно видалили акаунт!");
                    logForm.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Сталася помилка");
                }
            }
        }
    }
}
