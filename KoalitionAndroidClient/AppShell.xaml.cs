using KoalitionAndroidClient.ViewModels;
using KoalitionAndroidClient.Views.Menu;

namespace KoalitionAndroidClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            this.BindingContext = new AppShellViewModel();
        }
    }
}