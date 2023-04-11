using KoalitionAndroidClient.ViewModels.Menu;

namespace KoalitionAndroidClient.Views.Menu;

public partial class MenuPage : ContentPage
{
	public MenuPage(MenuPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}