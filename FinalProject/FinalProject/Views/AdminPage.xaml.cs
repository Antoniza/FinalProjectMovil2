﻿using FinalProject.ViewModels;
using FinalProject.Views.AdminViews;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : ContentPage
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";

        [Obsolete]
        public AdminPage()
        {
            InitializeComponent();
            BindingContext = new AdminViewModel();

            GetProfileInformationAndRefreshToken();
            ProductButton.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToProductsPanel()));
            UserButton.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToUsersPanel()));
        }

        public async void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                //This is the saved firebaseauthentication that was saved during the time of login
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                //Here we are Refreshing the token
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
                //Now lets grab user information
                UserName.Text = Preferences.Get("Username", "UserNoDefined");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Alerta", "La sesión ya ha terminado", "OK");
            }
        }

        private void ToProductsPanel()
        {
            Navigation.PushAsync(new ProductForm());
        }

        private void ToUsersPanel()
        {
            Navigation.PushAsync(new UserForm());
        }
    }
}