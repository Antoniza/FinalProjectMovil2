using FinalProject.Models;
using FinalProject.Providers;
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
    public partial class CategorizedProducts : ContentPage
    {
        Products selectedProduct;
        String category;

        [Obsolete]
        public CategorizedProducts(string _category)
        {
            InitializeComponent();

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
            ProductDetailId.Text = product.Id;
            ProductDetailImage.Source = product.Image;

            selectedProduct = product;

            ProductDetailPrice.Text = product.Price + " Lps";

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

        private void CancelBuyButton_Clicked(object sender, EventArgs e)
        {
            CloseModal();
        }

        private void PoundAndUnityText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var actual = ProductDetailPrice.Text;
            ProductDetailPrice.Text = Convert.ToString((Convert.ToDouble(actual) * Convert.ToDouble(PoundAndUnityText.Text)) + " Lps");
        }

        private void BuyButton_Clicked(object sender, EventArgs e)
        {
            List < Shopping > car = new List<Shopping>();
            var newShop = new Shopping
            {
                Image = selectedProduct.Image,
                ProductName = selectedProduct.Name,

                Quantity = PoundAndUnityText.Text + " Lbs/U"
            };

            car.Add(newShop);

            String shoppingCar = JsonConvert.SerializeObject(car);

            Preferences.Set("ShoppingCar", shoppingCar);
        }
    }
}