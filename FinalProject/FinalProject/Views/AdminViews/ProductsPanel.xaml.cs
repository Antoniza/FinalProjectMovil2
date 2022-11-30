using FinalProject.Models;
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
        Products selectedProduct;
        [Obsolete]
        public ProductForm()
        {
            InitializeComponent();

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
            AddNewProduct.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToAddProductPanel()));
            ClosePopUpModal.GestureRecognizers.Add(new TapGestureRecognizer((view) => CloseModal()));

            PopUpModal.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            var ProductsList = await ProductProvider.GetAllProducts();
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = ProductsList;
            ProductListView.IsRefreshing = false;

        }
        void ToAddProductPanel()
        {
            Navigation.PushAsync(new AddNewProductPanel());
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
        private void CloseModal()
        {
            PopUpModal.IsVisible = false;
        }

        private async void DeleteProduct()
        {
            String id = ProductDetailId.Text;
            bool res = await ProductProvider.DeleteProduct(id);

            if (res)
            {
                await DisplayAlert("Satisfactorio", "Se elimino el producto.", "OK");
                CloseModal();
            }
            else
            {
                await DisplayAlert("Fallo del sistema", "No se pudo eliminar el producto.", "OK");
            }
        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            CloseModal();
            Navigation.PushAsync(new EditProductPanel(selectedProduct));
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            DeleteProduct();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keywords = SearchBar.Text;
            var ProductList = await ProductProvider.GetAllProducts();
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