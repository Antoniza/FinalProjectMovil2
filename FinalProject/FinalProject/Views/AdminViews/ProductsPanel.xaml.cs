using FinalProject.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductForm : ContentPage
    {
        public ProductForm()
        {
            InitializeComponent();

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var ProductsList = await ProductProvider.GetAllProducts();
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = ProductsList;
            ProductListView.IsRefreshing = false;

        }

    }
}