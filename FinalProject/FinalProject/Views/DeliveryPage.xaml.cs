using FinalProject.Models;
using FinalProject.Providers;
using FinalProject.ViewModels;
using FinalProject.Views.DeliveryViews;
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
    public partial class DeliveryPage : ContentPage
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        public DeliveryPage()
        {
            InitializeComponent();
            BindingContext = new DeliveryViewModel();

            HistoryListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });

            GetProfileInformationAndRefreshToken();

            MenuButton.GestureRecognizers.Add(new TapGestureRecognizer((view) => OpenModal()));
            ClosePopUpModal.GestureRecognizers.Add(new TapGestureRecognizer((view) => CloseModal()));

            PopUpModal.IsVisible = false;
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
                UserEmail.Text = Preferences.Get("Email", "NoEmailFounded");
                UserPhone.Text = Preferences.Get("Phone", "00000000");
                UserImage.Source = Preferences.Get("Image", "https://i.ibb.co/vhh0Gkj/users.png");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Alerta", "La sesión ya ha terminado", "OK");
            }
        }

        private void OpenModal()
        {
            PopUpModal.IsVisible = true;
        }

        private void CloseModal()
        {
            PopUpModal.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            var historyList = await SalesProvider.GetPendingSales();
            HistoryListView.ItemsSource = null;
            HistoryListView.ItemsSource = historyList;
            HistoryListView.IsRefreshing = false;
        }

        private void HistoryListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var sale = e.Item as Sales;
            Navigation.PushAsync(new TaskSalePage(sale));
        }
    }
}