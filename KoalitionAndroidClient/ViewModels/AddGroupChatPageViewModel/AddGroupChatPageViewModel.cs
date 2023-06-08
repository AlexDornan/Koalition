using KoalitionAndroidClient.Helpers;
using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.ViewModels.Menu;
using KoalitionAndroidClient.Views.Menu;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;

namespace KoalitionAndroidClient.ViewModels.AddGroupChatPageViewModel
{
    public class AddGroupChatPageViewModel:BaseViewModel
    {
        private ObservableCollection<CreateGroupChatRequest> _groupChat;
        public ObservableCollection<CreateGroupChatRequest> GroupChat
        {
            get  => _groupChat; 
            set
            {
                _groupChat = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        private string _description;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        public ICommand CreateGroupChatCommand { get; set; }
        public AddGroupChatPageViewModel()
        {
            CreateGroupChatCommand = new Command<object>(async (param) => await CreateGroupChat());
        }

        public async Task CreateGroupChat()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);
            var groupChat = new CreateGroupChatRequest
            {
                Name = Name,
                Description = Description
            };

            var groupChatJson = JsonConvert.SerializeObject(groupChat);
            var content = new StringContent(groupChatJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ApiPlatformUrlHelper.GetPlatformApiUrl()}/api/GroupChat/createGroupChat", content);

            await Shell.Current.GoToAsync("///MenuPage");
            if (Application.Current.MainPage is MenuPage menuPage && menuPage.BindingContext is MenuPageViewModel menuViewModel)
            {
                menuViewModel.GetGroupChats();
                await Application.Current.MainPage.Navigation.PushAsync(menuPage);
            }
            else
            {
                Console.WriteLine("Error: Unable to cast MainPage to MenuPage or BindingContext to MenuPageViewModel.");
            }
        }

    }
}
