using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.ViewModels.Chat;
using KoalitionAndroidClient.ViewModels.Menu;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KoalitionAndroidClient.Views.Chat;

public partial class GroupChatPage : ContentPage
{
    public GroupChatPage(GroupChatPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}