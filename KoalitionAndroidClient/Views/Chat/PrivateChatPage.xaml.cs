using KoalitionAndroidClient.ViewModels.Chat;
using System.Web;

namespace KoalitionAndroidClient.Views.Chat;

public partial class PrivateChatPage : ContentPage
{
	public PrivateChatPage(PrivateChatPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}