using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class loggin : Form
    {
        public loggin()
        {
            InitializeComponent();
            clearTexBox();
        }

        private void clearTexBox()
        {
            textBox4.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearTexBox();
            this.Close();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
              if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
              { MessageBox.Show("Заповніть поля!"); }
              else
              {
                      HttpClient httpClient = new HttpClient();
                      LoginRequest loginRequest = new LoginRequest();
                      loginRequest.Login = textBox4.Text.ToString();
                      loginRequest.Password = textBox3.Text.ToString();
                      var json = Newtonsoft.Json.JsonConvert.SerializeObject(loginRequest);
                      var content = new StringContent(json, Encoding.UTF8, "application/json");
                      var response = await httpClient.PostAsync("https://localhost:7127/api/Users/login", content);
                      if (response.IsSuccessStatusCode)
                      {
                          LoginResponse loginResponse = new LoginResponse();
                          var result = await response.Content.ReadAsStringAsync();
                          loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
                          MessageBox.Show("Ви успішно Авторизувалися! Вітаю - " + loginResponse.userDetails.name.ToString());
                          clearTexBox();
                          this.Close();
                          myChat formChat = new myChat(loginResponse, textBox3.Text);
                          formChat.ShowDialog();
                       }
                      else
                      {
                          MessageBox.Show("Сталася помилка!");
                      }
              }
        }
    }
}
