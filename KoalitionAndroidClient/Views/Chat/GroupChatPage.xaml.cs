using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.ViewModels.Chat;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KoalitionAndroidClient.Views.Chat;

public partial class GroupChatPage : ContentPage
{
    public GroupChatPage(GroupChatResponce groupChatResponse, LoginResponse loginResponse)
    {
        InitializeComponent();
        BindingContext = new GroupChatPageViewModel(groupChatResponse, loginResponse);
    }
}