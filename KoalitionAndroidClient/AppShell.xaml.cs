﻿using KoalitionAndroidClient.ViewModels;
using KoalitionAndroidClient.Views.AddGroupChatPage;
using KoalitionAndroidClient.Views.Chat;
using KoalitionAndroidClient.Views.Menu;

namespace KoalitionAndroidClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            this.BindingContext = new AppShellViewModel();
            
            Routing.RegisterRoute("MenuPage", typeof(MenuPage));
            Routing.RegisterRoute("AddGroupChatPage", typeof(AddGroupChatPage));
            Routing.RegisterRoute("PrivateChatPage", typeof(PrivateChatPage));
        }
    }
}