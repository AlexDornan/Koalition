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
        public MenuPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            //GetGroupChats();
        }

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
    }
}
