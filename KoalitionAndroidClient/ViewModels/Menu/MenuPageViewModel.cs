using KoalitionAndroidClient.Controls;
using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.Services;
using KoalitionAndroidClient.ViewModels.Chat;
using KoalitionAndroidClient.Views.AddGroupChatPage;
using KoalitionAndroidClient.Views.Chat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KoalitionAndroidClient.ViewModels.Menu
{
    public class MenuPageViewModel : BaseViewModel
    {
        public ObservableCollection<GroupChatResponse> GroupChats { get; set; } = 
            new ObservableCollection<GroupChatResponse>();
        private ObservableCollection<UserBasicInfo> _users;
        public ObservableCollection<UserBasicInfo> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public readonly ILoginService _loginService;
        private ObservableCollection<ChatMessageResponse> _messages;
        public GroupChatResponse _selectedGroupChat;

        public ObservableCollection<ChatMessageResponse> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        public ICommand EnterChatCommand => new Command(EnterChat);
        public ICommand EnterCreateGroupChatCommand => new Command(EnterCreateGroupChat);
        //public ICommand EnterPrivateChatCommand => new Command<UserBasicInfo>(async (recipient) => await EnterPrivateChat(recipient));
        //public ICommand EnterPrivateChatCommand => new Command(async () => await EnterPrivateChat());
        //public ICommand EnterPrivateChatCommand => new Command(EnterPrivateChat);
        public ICommand EnterPrivateChatCommand => new Command(EnterPrivateChat);
        public MenuPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            //GetGroupChats();
            GetUsers();
        }

        //надо будет как-то передавать айди открытого юзера в чат и сообщения
        public async void EnterPrivateChat(object selectedUser)
        {
            var user = selectedUser as UserBasicInfo;

            var viewModel = new PrivateChatPageViewModel(user);
         
            var privateChatPage = new PrivateChatPage(viewModel);
            await Shell.Current.Navigation.PushAsync(privateChatPage);
        }

        /*private async void EnterPrivateChat(UserBasicInfo privateChat)
        {
            await Shell.Current.GoToAsync($"PrivateChatPage?recipientId={privateChat.UserId}");
        }*/


        public async void EnterChat(object groupChat)
        {
            var selectedGroupChat = groupChat as GroupChatResponse;

            var viewModel = new GroupChatPageViewModel(selectedGroupChat);
            await viewModel.GetUsersAndMessages();

            var groupChatPage = new GroupChatPage(viewModel);
            await Shell.Current.Navigation.PushAsync(groupChatPage);
        }

        public void GetGroupChats()
        {
            Task.Run(async () =>
            {
                var groupChatList = await _loginService.GetGroupChats();

                App.Current.Dispatcher.Dispatch(() =>
                {
                    if (groupChatList?.Count > 0)
                    {
                        foreach (var groupChat in groupChatList)
                        {
                            GroupChats.Add(groupChat);
                        }
                    }
                });
            });
        }

        public async void EnterCreateGroupChat()
        {
            await Shell.Current.GoToAsync("AddGroupChatPage");
        }

        private async Task GetUsers()
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            var response = await httpClient.GetAsync("http://10.0.2.2:5127/api/Users/allUsers");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserBasicInfo>>(content);
                Users = new ObservableCollection<UserBasicInfo>(users);
            }
            else
            {
                // Handle error response
            }
        }

    }
}
