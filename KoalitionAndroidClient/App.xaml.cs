using KoalitionAndroidClient.Models;

namespace KoalitionAndroidClient
{
    public partial class App : Application
    {
        public static UserBasicInfo UserDetails;
        public static string Token;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}