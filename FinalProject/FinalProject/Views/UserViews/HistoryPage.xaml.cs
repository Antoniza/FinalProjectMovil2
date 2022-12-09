using FinalProject.Models;
using FinalProject.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            HistoryListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var historyList = await SalesProvider.GetUserHistory(Preferences.Get("Email", ""));
            HistoryListView.ItemsSource = null;
            HistoryListView.ItemsSource = historyList;
            HistoryListView.IsRefreshing = false;
        }

        private void HistoryListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var history = e.Item as Sales;
            Navigation.PushAsync(new HistoryDetailPage(history));
        }
    }
}