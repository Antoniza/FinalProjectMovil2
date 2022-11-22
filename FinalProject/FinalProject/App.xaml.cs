using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FinalProject.Views;
using Xamarin.Essentials;

namespace FinalProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")) && Preferences.Get("Level","") == "C")
            {
                MainPage = new NavigationPage(new ClientPage());
            }
            else if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")) && Preferences.Get("Level", "") == "D")
            {
                MainPage = new NavigationPage(new DeliveryPage());
            }
            else if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")) && Preferences.Get("Level", "") == "A")
            {
                MainPage = new NavigationPage(new AdminPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
