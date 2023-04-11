using KoalitionAndroidClient.Controls;
using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.ViewModels.Menu
{
    public class MenuPageViewModel : BaseViewModel
    {
        public ObservableCollection<GroupChatResponce> GroupChats { get; set; } = 
            new ObservableCollection<GroupChatResponce>();

        private readonly ILoginService _loginService;

        public MenuPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            GetGroupChats();
        }

        private void GetGroupChats()
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
