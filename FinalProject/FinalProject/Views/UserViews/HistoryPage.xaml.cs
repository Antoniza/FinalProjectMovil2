using FinalProject.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
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

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var historyList = await SalesProvider.GetUserHistory(Preferences.Get("Email", ""));
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = historyList;
            ProductListView.IsRefreshing = false;
        }
    }
}