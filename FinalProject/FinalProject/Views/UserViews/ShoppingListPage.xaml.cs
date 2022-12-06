using FinalProject.Models;
using Newtonsoft.Json;
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
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();
            lol.Text = Preferences.Get("ShoppingCar", "NoToken");

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override void OnAppearing()
        {
            String list = Preferences.Get("ShoppingCar", "");
            var ShoppingList = JsonConvert.DeserializeObject<List<Shopping>>(list);
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = ShoppingList;
            ProductListView.IsRefreshing = false;
        }
    }
}