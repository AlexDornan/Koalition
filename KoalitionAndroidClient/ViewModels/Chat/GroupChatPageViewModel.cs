using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;

namespace KoalitionAndroidClient.ViewModels.Chat
{
    public class GroupChatPageViewModel : BaseViewModel
    {
        private ObservableCollection<ChatMessageResponse> _messages;
        private ObservableCollection<SendMessageRequest> _sendMessageText;
        private ObservableCollection<UserBasicInfo> _users;
        private GroupChatResponse _selectedGroupChat;
        public GroupChatResponse SelectedGroupChat { get; set; }
        

        public ObservableCollection<ChatMessageResponse> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public ObservableCollection<UserBasicInfo> User
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ObservableCollection<SendMessageRequest> SendMessageRequests
        {
            get => _sendMessageText;
            set
            {
                _sendMessageText = value;
                OnPropertyChanged();
            }
        }

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                _newMessage = value;
                OnPropertyChanged(nameof(NewMessage));
            }
        }

        private string _editMessage;
        public string EditMessageText
        {
            get { return _editMessage; }
            set
            {
                _editMessage = value;
                OnPropertyChanged(nameof(EditMessageText));
                IsEditing = !string.IsNullOrEmpty(_editMessage);
            }
        }




        public ObservableCollection<UserBasicInfo> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(EditMessageText));
            }
        }

        private ChatMessageResponse _selectedMessage;
        public ChatMessageResponse SelectedMessage
        {
            get { return _selectedMessage; }
            set
            {
                _selectedMessage = value;
                OnPropertyChanged(nameof(SelectedMessage));
                IsEditing = (_selectedMessage != null);
            }
        }

        public ICommand SendMessageCommand { get;set; }
        public ICommand EditMessageCommand { get; }
        public GroupChatPageViewModel(GroupChatResponse selectedGroupChat)
        {
            //EditMessageCommand = new Command(async () => await EditSelectedMessage());
            EditMessageCommand = new Command<ChatMessageResponse>(EditMessage);
            SendMessageCommand = new Command(async () =>
            {
                if (IsEditing)
                {
                    // Edit the selected message
                    await EditSelectedMessage();
                }
                else
                {
                    // Send a new message
                    await SendMessage();
                }
            });

            _selectedGroupChat = selectedGroupChat;
            GetUsersAndMessages();
        }


        public void EditMessage(ChatMessageResponse message)
        {
            // без этоуй хуйни выбранное сообщение пустое, с ней — выбраное и хз как его изменить
            EditMessageText = message.Text;
            SelectedMessage = message;
            SelectedMessage.MessageId = message.MessageId;
            IsEditing = true;
        }

        public async Task EditSelectedMessage()
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);


            var GroupChatId = _selectedGroupChat.Id;
            var MessageId = SelectedMessage.MessageId;
                //Text = EditMessageText, // Use the updated message text from the view model
            
            var text = EditMessageText;

            var message = new SendMessageRequest { Text = text };
            var messageJson = JsonConvert.SerializeObject(message);
            var content = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"http://10.0.2.2:5127/api/groupchats/{_selectedGroupChat.Id}/messages/{SelectedMessage.MessageId}", content);

            if (response.IsSuccessStatusCode)
            {
                // Clear the EditMessageText property
                EditMessageText = string.Empty;
                SelectedMessage = null;
                IsEditing = false;
                await GetUsersAndMessages();
            }
        }



        public async Task GetUsersAndMessages()
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            // Get all users
            var usersResponse = await httpClient.GetAsync("http://10.0.2.2:5127/api/Users/allUsers");
            var usersJson = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserBasicInfo>>(usersJson);

            var messagesResponse = await httpClient.GetAsync($"http://10.0.2.2:5127/api/groupchats/{_selectedGroupChat.Id}/messages");
            var messagesJson = await messagesResponse.Content.ReadAsStringAsync();
            var messages = JsonConvert.DeserializeObject<List<ChatMessageResponse>>(messagesJson);

            foreach (var message in messages)
            {
                var sender = users.FirstOrDefault(u => u.UserId == message.UserId);
                if (sender != null)
                {
                    message.Name = sender.Name;
                }
            }
            Messages = new ObservableCollection<ChatMessageResponse>(messages);
        }
        

        public async Task SendMessage()
        {
            

            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);
            var message = new SendMessageRequest
            {
                Text = NewMessage,
                Id = _selectedGroupChat.Id
            };
            var messageJson = JsonConvert.SerializeObject(message);
            var content = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"http://10.0.2.2:5127/api/groupchats/{_selectedGroupChat.Id}/messages/sendMessage", content);
            if (response.IsSuccessStatusCode)
            {
                // Clear the NewMessage property
                NewMessage = string.Empty;
                await GetUsersAndMessages();
            }

        }

        
    }
}

