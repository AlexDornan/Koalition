using KoalitionAndroidClient.Controls;
using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.Services;
using KoalitionAndroidClient.ViewModels.Chat;
using KoalitionAndroidClient.Views.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KoalitionAndroidClient.ViewModels.Menu
{
    public class MenuPageViewModel : BaseViewModel
    {
        public ObservableCollection<GroupChatResponce> GroupChats { get; set; } = 
            new ObservableCollection<GroupChatResponce>();

        public readonly ILoginService _loginService;

        public ICommand EnterChatCommand => new Command(EnterChat);
        public MenuPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            GetGroupChats();

            
        }
        public async void EnterChat(object groupChat)
        {
            // Cast the groupChat parameter to GroupChatResponce
            var selectedGroupChat = groupChat as GroupChatResponce;

            // Navigate to GroupChatPage with the selected GroupChat as parameter
            await Shell.Current.GoToAsync($"//{nameof(GroupChatPage)}?{nameof(GroupChatPageViewModel.SelectedGroupChat)}={selectedGroupChat.Id}");
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
    }
}
