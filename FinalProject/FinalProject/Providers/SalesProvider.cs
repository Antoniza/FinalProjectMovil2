using FinalProject.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FinalProject.Providers
{
    class SalesProvider
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        public static FirebaseClient firebase = new FirebaseClient("https://store-c5904-default-rtdb.firebaseio.com/");

        //==============================================================================

        //==============================================================================
        public static async Task<bool> AddSale(Sales sale)
        {
            try
            {
                await firebase
                .Child("Sales")
                .PostAsync(sale);
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
        public static async Task<List<Sales>> GetUserHistory(string email)
        {
            return (await firebase.Child(nameof(Sales)).OnceAsync<Sales>()).Select(item => new Sales
            {
                Id = item.Key,
                ClientName = item.Object.ClientName,
                ClientMail = item.Object.ClientMail,
                ClientPhone = item.Object.ClientPhone,
                ClientLatitude = item.Object.ClientLatitude,
                ClientLongitude = item.Object.ClientLongitude,
                TotalToPay = item.Object.TotalToPay,
                Detail = item.Object.Detail,
                Date = item.Object.Date,
                PayFormat = item.Object.PayFormat,
                ShoppingCar = item.Object.ShoppingCar,
                State = item.Object.State
            }).Where(h => h.ClientMail.ToLower().Contains(Preferences.Get("Email", "").ToLower())).ToList();
        }
    }
}
