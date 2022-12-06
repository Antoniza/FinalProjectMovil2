using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Providers
{
    class ShoppingProvider
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        public static FirebaseClient firebase = new FirebaseClient("https://store-c5904-default-rtdb.firebaseio.com/");
    }
}
