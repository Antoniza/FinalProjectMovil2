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

            ClosePopUpModal.GestureRecognizers.Add(new TapGestureRecognizer((view) => CloseModal()));
            LikeButton.GestureRecognizers.Add(new TapGestureRecognizer((view) => LikeMessage()));
        }

        private void LikeMessage()
        {
            DisplayAlert("Calificación", "Gracias por marcar este producto como favorito.", "Un Placer");
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
            PoundAndUnityText.Text = "1";
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

            if(Preferences.Get("ShoppingCar", "") == "")
            {
                List<Shopping> car = new List<Shopping>();
                var newShop = new Shopping
                {
                    Image = selectedProduct.Image,
                    ProductName = selectedProduct.Name,
                    TotalShop = ProductDetailPrice.Text,
                    Quantity = PoundAndUnityText.Text
                };

                car.Add(newShop);

                String shoppingCar = JsonConvert.SerializeObject(car);

                Preferences.Set("ShoppingCar", shoppingCar);

            }
            else
            {
                String list = Preferences.Get("ShoppingCar", "");
                var items = JsonConvert.DeserializeObject<List<Shopping>>(list);
                List<Shopping> car = new List<Shopping>();
                for (int i = 0; i < items.Count(); i++)
                {
                    car.Add(items[i]);
                }

                var newShop = new Shopping
                {
                    Image = selectedProduct.Image,
                    ProductName = selectedProduct.Name,
                    TotalShop = ProductDetailPrice.Text,
                    Quantity = PoundAndUnityText.Text
                };

                car.Add(newShop);
                String shoppingCar = JsonConvert.SerializeObject(car);

                Preferences.Set("ShoppingCar", shoppingCar);
            }
            DisplayAlert("Listo", $"Se agrego {selectedProduct.Name} al carrito.", "OK");
        }

        private void PoundAndUnityText_Completed(object sender, EventArgs e)
        {
            ProductDetailPrice.Text = Convert.ToString((Convert.ToDouble(selectedProduct.Price) * Convert.ToDouble(PoundAndUnityText.Text)) + " Lps");
        }
    }
}