using FinalProject.Models;
using FinalProject.Views;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FinalProject.Providers
{
    class ProductProvider
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        public static FirebaseClient firebase = new FirebaseClient("https://store-c5904-default-rtdb.firebaseio.com/");
        public static FirebaseStorage firebaseStorage = new FirebaseStorage("store-c5904.appspot.com");
        //==============================================================================

        //==============================================================================
        public static async Task<List<Products>> GetAllProducts()
        {
            try
            {
                var productsList = (await firebase
                .Child("Products")
                .OnceAsync<Products>()).Select(item =>
                new Products
                {
                    Name = item.Object.Name,
                    Price = item.Object.Price,
                    Category = item.Object.Category,
                    Description = item.Object.Description,
                    Image = item.Object.Image,
                }).ToList();
                return productsList;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        //==============================================================================

        //==============================================================================
        public static async Task<bool> AddProduct(string name, string price, string description, string category, string image)
        {
            try
            {
                await firebase
                .Child("Products")
                .PostAsync(new Products() { Name = name, Price = price, Description = description, Category = category, Image = image});
                return true;
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", e.Message, "OK");
                return false;
            }
        }
        //==============================================================================

        //==============================================================================
        public static async Task<Products> GetProduct(string productName)
        {
            try
            {
                var allProducts = await GetAllProducts();
                await firebase
                .Child("Products")
                .OnceAsync<Products>();
                return allProducts.Where(a => a.Name == productName).FirstOrDefault();

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //==============================================================================

        //==============================================================================
        public async void CreateNewProduct(string name, string price, string description, string category, string image)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(image))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Se requiere llenar todos los campos del formulario.", "OK");
            }
            else
            {
                try
                {
                    await AddProduct(name, price, description, category, image);
                }
                catch (Exception ex)
                {
                }
            }
        }

        //==============================================================================

        //==============================================================================

        public static async Task<string> SaveImage(Stream image, string filename)
        {
            var img = await firebaseStorage.Child("ProductsImages").Child(filename).PutAsync(image);
            return img;
        }
    }
}
