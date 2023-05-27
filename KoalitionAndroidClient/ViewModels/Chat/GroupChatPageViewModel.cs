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

        public ObservableCollection<UserBasicInfo> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand SendMessageCommand { get;set; }
        public GroupChatPageViewModel(GroupChatResponse selectedGroupChat)
        {
            SendMessageCommand = new Command<object>(async (param) => await SendMessage());
            //_sendMessageText = new ObservableCollection<SendMessageRequest>();
            _selectedGroupChat = selectedGroupChat;
            GetUsersAndMessages();
        }

        public async Task GetUsersAndMessages()
        {
            if (_selectedGroupChat == null)
            {
                Console.WriteLine("Error: SelectedGroupChat is null.");
                return;
            }

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
        
        //юзануть этот принцип в создании чата
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

                // Refresh the messages
                await GetUsersAndMessages();
            }
        }
    }
}

