using FinalProject.Models;
using FinalProject.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategorizedProducts : ContentPage
    {
        Products selectedProduct;
        String category;

        [Obsolete]
        public CategorizedProducts(string _category)
        {
            InitializeComponent();

            ClosePopUpModal.GestureRecognizers.Add(new TapGestureRecognizer((view) => CloseModal()));

            category = _category;
            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
            CloseModal();
        }

        protected override async void OnAppearing()
        {
            var ProductsList = await ProductProvider.GetAllByCategory(category);
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = ProductsList;
            ProductListView.IsRefreshing = false;

        }

        private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var product = e.Item as Products;
            ProductDetailName.Text = product.Name;
            ProductDetailDescription.Text = product.Description;
            ProductDetailPrice.Text = product.Price + " Lps";
            ProductDetailId.Text = product.Id;
            ProductDetailImage.Source = product.Image;

            selectedProduct = product;

            PopUpModal.IsVisible = true;
        }
        private void OpenModal()
        {
            PopUpModal.IsVisible = true;
        }

        private void CloseModal()
        {
            PopUpModal.IsVisible = false;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keywords = SearchBar.Text;
            var ProductList = await ProductProvider.GetAllByCategory(category);
            if (string.IsNullOrEmpty(keywords))
            {
                ProductListView.ItemsSource = ProductList;
            }
            else
            {
                ProductListView.ItemsSource = ProductList.Where(Names => Names.Name.ToLower().Contains(keywords.ToLower()));
            }
        }
    }
}