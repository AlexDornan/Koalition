using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using Newtonsoft.Json;
using WindowsFormsApp1.Users;

namespace WindowsFormsApp1
{
    public partial class myChat : Form
    {
        LoginResponse loginResponse;
        string pass, selectedValue, selectedItem;
        int idChatRez;
        Boolean myFlag = false;
        private Timer timer;

        public myChat()
        {
            InitializeComponent();
        }

        public myChat(LoginResponse loginResponse, string pass, Form log)
        {
            InitializeComponent();
            log.Close();
            this.tabControl1.Visible = false;
            this.loginResponse = loginResponse;
            this.pass = pass;
            label1.Text = loginResponse.userDetails.name.ToString();
            myChats();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            if (tabControl1.SelectedTab == tabPage2)
            {
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Виконання методу, якщо вибрано вкладку page2
            if (tabControl1.SelectedTab == tabPage2)
            {
                GetUsersAndMessages();
            }
        }

        private async void myChats()
        {
            listBox1.Items.Clear();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResponse.Token);

            var response = await client.GetAsync("https://localhost:7127/api/GroupChat/getChats");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    List<chatUser> chats = JsonConvert.DeserializeObject<List<chatUser>>(result);
                    listBox1.DisplayMember = "Value";
                    listBox1.ValueMember = "Key";
                    foreach (chatUser chat in chats)
                    {
                        var pair = new KeyValuePair<int, string>(chat.id, chat.name);
                        listBox1.Items.Add(pair);
                    }
                }
            }
            else
            {
                MessageBox.Show("Сталася помилка при спробі вивести Ваші чати!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            profil profils = new profil(loginResponse, this, pass);
            profils.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.tabControl1.Visible = true;
            this.tabControl1.SelectedTab = tabPage1;
            allUser(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private async void allUser(int flag)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResponse.Token);

            var response = await client.GetAsync("https://localhost:7127/api/Users/allUsers");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(data);

                if (flag == 0)
                {
                    dataGridView1.DataSource = users;
                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Заповніть поля!");
            }
            else
            {
                myChats();
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                var response = await client.GetAsync("https://localhost:7127/api/GroupChat/getChats");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        List<chatUser> chats = JsonConvert.DeserializeObject<List<chatUser>>(result);
                        foreach (chatUser chat in chats)
                        {
                            if(chat.name == textBox1.Text)
                            {
                              MessageBox.Show("Такий чат існує!");
                               myFlag = true;
                              break;
                            }
                            else
                            {
                                myFlag = false;
                            }
                        }
                        if(myFlag == false)
                        {
                            HttpClient httpClient = new HttpClient();
                            chatUser chatUsers = new chatUser();
                            chatUsers.name = textBox1.Text.ToString();
                            chatUsers.description = textBox2.Text.ToString();
                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(chatUsers);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            httpClient.DefaultRequestHeaders.Authorization =
                                           new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                            var responseT = await httpClient.PostAsync("https://localhost:7127/api/GroupChat/createGroupChat", content);

                            if (responseT.IsSuccessStatusCode)
                            {
                                HttpClient clients = new HttpClient();

                                clients.DefaultRequestHeaders.Authorization =
                                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                                var responses = await clients.GetAsync("https://localhost:7127/api/GroupChat/getChats");

                                if (responses.IsSuccessStatusCode)
                                {
                                    var results = await responses.Content.ReadAsStringAsync();
                                    if (results != null)
                                    {
                                        List<chatUser> chatsS = JsonConvert.DeserializeObject<List<chatUser>>(results);
                                        foreach (chatUser chatS in chatsS)
                                        {
                                            if (chatS.name == textBox1.Text)
                                            {
                                                idChatRez = chatS.id;
                                            }
                                        }

                                        List<int> selectedIds = new List<int>();

                                        // проходим по всем строкам DataGridView и добавляем id выбранных пользователей
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["che"];
                                            if (chk.Value != null && (bool)chk.Value)
                                            {
                                                int id = (int)row.Cells["userId"].Value;
                                                selectedIds.Add(id);
                                            }

                                        }

                                        for(int i = 0; i < selectedIds.Count; i++)
                                        {
                                            HttpClient clientNew = new HttpClient();
                                            clientNew.DefaultRequestHeaders.Authorization =
                                                        new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                                            clientNew.BaseAddress = new Uri("https://localhost:7127/");
                                            UserInChat userA = new UserInChat();
                                            userA.groupChatId = idChatRez;
                                            userA.userId = selectedIds[i];
                                            var jsonS = Newtonsoft.Json.JsonConvert.SerializeObject(userA);
                                            var contentS = new StringContent(jsonS, Encoding.UTF8, "application/json");
                                            HttpResponseMessage responseNew = clientNew.PostAsync("api/GroupChat/addUser", contentS).Result;
                                            if (responseNew.IsSuccessStatusCode && i == selectedIds.Count - 1)
                                            {
                                                MessageBox.Show("Чат успішно створено! Друзі додані!");
                                                myChats();
                                                this.tabControl1.Visible = false;
                                            }
                                            else
                                            {
                                                if(i == selectedIds.Count - 1)
                                                  MessageBox.Show("Помилка при додавані друзів: " + responseNew.StatusCode);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Помилка!");
                            }
                        }
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string selected = listBox1.SelectedItem.ToString();
                char[] charsToTrim = { '[', ']' };
                selected = selected.TrimStart(charsToTrim).TrimEnd(charsToTrim);
                string[] parts = selected.Split(',');
                selectedItem = parts[0].Trim();
                selectedValue = parts[1].Trim();
                this.tabControl1.Visible = true;
                this.tabControl1.SelectedTab = tabPage2;
                label5.Text = selectedValue;
                GetUsersAndMessages();
                // Запуск таймера
                timer.Start();
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResponse.Token);

            string url = "https://localhost:7127/api/GroupChat/" + selectedValue;

            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Чат {selectedValue} успішно видалено.");
                myChats();
                this.tabControl1.Visible = false;
            }
            else
            {
                MessageBox.Show($"При видалені чату {selectedValue} сталася помилка.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) || !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                string url = "https://localhost:7127/api/groupchats/" + selectedItem + "/messages/sendMessage";

                mess messS = new mess();
                messS.text = textBox3.Text;

                var jsonS = Newtonsoft.Json.JsonConvert.SerializeObject(messS);
                var contentS = new StringContent(jsonS, Encoding.UTF8, "application/json");

                HttpResponseMessage responseNew = client.PostAsync(url, contentS).Result;

                if (!responseNew.IsSuccessStatusCode) 
                {
                    MessageBox.Show("Сталася помилка.");
                }
                textBox3.Text = "";
            }
        }


        private async void GetUsersAndMessages()
        {
            HttpClient _httpClient = new HttpClient();
            string _baseUrl = "https://localhost:7127/api/";

            _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

            // Получаем всех пользователей
            var usersResponse = await _httpClient.GetAsync(_baseUrl + "Users/allUsers");
            var usersJson = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            // Получаем все сообщения указанного чата
            var messagesResponse = await _httpClient.GetAsync(_baseUrl + $"groupchats/{selectedItem}/messages");
            var messagesJson = await messagesResponse.Content.ReadAsStringAsync();
            var messages = JsonConvert.DeserializeObject<List<mess>>(messagesJson);

            if(listBox2.Items.Count != messages.Count) 
            {
                listBox2.Items.Clear();

                foreach (var message in messages)
                {
                    foreach (var user in users)
                    {
                        if (message.userId == user.userId)
                        {
                            int dotIndex = message.time.IndexOf(".");
                            if (dotIndex >= 0)
                            {
                                message.time = message.time.Substring(0, dotIndex);
                            }
                            listBox2.Items.Add($"{message.time} - {user.name} : {message.text}");
                            break;
                        }
                    }
                }
            }
        }




    }
}
