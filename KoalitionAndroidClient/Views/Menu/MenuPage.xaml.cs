using KoalitionAndroidClient.ViewModels.Menu;

namespace KoalitionAndroidClient.Views.Menu;

public partial class MenuPage : ContentPage
{
    private MenuPageViewModel _viewModel;

    public MenuPage(MenuPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GroupChats.Clear();
        _viewModel.GetGroupChats();
    }
}