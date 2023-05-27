using KoalitionAndroidClient.ViewModels.AddGroupChatPageViewModel;

namespace KoalitionAndroidClient.Views.AddGroupChatPage;

public partial class AddGroupChatPage : ContentPage
{
	public AddGroupChatPage(AddGroupChatPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}