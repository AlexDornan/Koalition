using KoalitionAndroidClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.ViewModels.Chat
{
    public class GroupChatPageViewModel : BaseViewModel
    {
        private GroupChatResponce _selectedGroupChat;
        private ObservableCollection<ChatMessageResponse> _messages;
        private string _entryMessageText;
        private LoginResponse _loginResponse;
        private ObservableCollection<UserBasicInfo> _users;



        public GroupChatResponce SelectedGroupChat
        {
            get => _selectedGroupChat;
            set
            {
                _selectedGroupChat = value;
                OnPropertyChanged();
            }
        }

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

        public string EntryMessageText
        {
            get => _entryMessageText;
            set
            {
                _entryMessageText = value;
                OnPropertyChanged();
            }
        }

        public Command SendMessageCommand { get; }

        public GroupChatPageViewModel(GroupChatResponce selectedGroupChat, LoginResponse loginResponse)
        {
            _selectedGroupChat = selectedGroupChat;
            Messages = new ObservableCollection<ChatMessageResponse>();

            SendMessageCommand = new Command(SendMessage);
            _loginResponse = loginResponse;

            GetUsersAndMessages();
        }

        public void SendMessage()
        {
            // Use _selectedGroupChat.Id and other properties to perform actions in the group chat
            // For example, send a message to the group chat using the selected group chat's ID
            int groupId = _selectedGroupChat.Id;

            // Perform action with the group chat ID
            // After sending the message, add it to the Messages collection to display in the chat
            ChatMessageResponse newMessage = new ChatMessageResponse
            {
                Text = EntryMessageText,
                Time = DateTime.Now,
                UserId = 1 // replace with the actual user ID
            };

            Messages.Add(newMessage);

            // Clear the entry after sending the message
            EntryMessageText = string.Empty;
        }

        private async void GetUsersAndMessages()
        {
            if (_selectedGroupChat == null)
            {
                // Return or throw an exception, depending on how you want to handle the null case
                return;
            }
            HttpClient _httpClient = new HttpClient();
            string _baseUrl = "http://10.0.2.2:5127/api/";

            // Set the authorization header with the bearer token
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _loginResponse.Token);

            // Get all users
            var usersResponse = await _httpClient.GetAsync("http://10.0.2.2:5127/api/Users/allUsers");
            var usersJson = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserBasicInfo>>(usersJson);

            // Get all messages for the selected group chat
            var messagesResponse = await _httpClient.GetAsync(_baseUrl + $"groupchats/{SelectedGroupChat.Id}/messages");
            var messagesJson = await messagesResponse.Content.ReadAsStringAsync();
            var messages = JsonConvert.DeserializeObject<List<ChatMessageResponse>>(messagesJson);

            if (Messages.Count != messages.Count)
            {
                Messages.Clear();

                foreach (var message in messages)
                {
                    var user = users.FirstOrDefault(u => u.UserId == message.UserId);
                    if (user != null)
                    {
                        Messages.Add(message); // Add ChatMessageResponse object to Messages collection
                    }
                }
            }
        }

    }
}

