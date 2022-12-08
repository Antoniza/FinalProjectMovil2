using FinalProject.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinalProject.Providers
{
    class SalesProvider
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        public static FirebaseClient firebase = new FirebaseClient("https://store-c5904-default-rtdb.firebaseio.com/");

        //==============================================================================

        //==============================================================================
        public static async Task<bool> AddSale(Sale sale)
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
    }
}
