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
    class UserProvider
    {
        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        public static FirebaseClient firebase = new FirebaseClient("https://store-c5904-default-rtdb.firebaseio.com/");
        public static FirebaseStorage firebaseStorage = new FirebaseStorage("store-c5904.appspot.com");
        //==============================================================================

        //==============================================================================
        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Id = item.Key,
                    Name = item.Object.Name,
                    Email = item.Object.Email,
                    Phone = item.Object.Phone,
                    Image = item.Object.Image,
                    Level = item.Object.Level,
                    Location = item.Object.Location,
                    Gender = item.Object.Gender,
                    Age = item.Object.Age
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        //==============================================================================

        //==============================================================================
        public static async Task<bool> AddUser(string name, string email, string phone)
        {
            try
            {
                await firebase
                .Child("Users")
                .PostAsync(new Users() { Name = name, Email = email, Phone = phone , Level = "C", Image = "https://i.ibb.co/vhh0Gkj/users.png", Gender= "", Location= "", Age = "" });
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
        public static async Task<Users> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
                
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        //==============================================================================

        //==============================================================================

        public async void EmailVerification(string email)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            await authProvider.SendEmailVerificationAsync(email);

            await Application.Current.MainPage.DisplayAlert("Verificación", "Se ha enviado a " + email + " un correo de verificación", "OK");
        }
        //==============================================================================

        //==============================================================================

        public static async Task<bool> UpdateUser(Users user)
        {
            await firebase.Child(nameof(Users) + "/" + user.Id).PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }

        //==============================================================================

        //==============================================================================
        public async void Login(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                var user = await GetUser(email);
                Preferences.Set("Id", user.Id);
                Preferences.Set("Username", user.Name);
                Preferences.Set("Email", user.Email);
                Preferences.Set("Phone", user.Phone);
                Preferences.Set("Level", user.Level);
                Preferences.Set("Image", user.Image);
                Preferences.Set("Gender", user.Gender);
                Preferences.Set("Location", user.Location);
                Preferences.Set("Age", user.Age);

                if (user.Level == "C")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new StartPage());
                }
                else if (user.Level == "D")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new DeliveryPage());
                }
                else if (user.Level == "A")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new AdminPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error de usuario", "No se le pudo asignar nivel a esta cuenta", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fallo de sesión", "Correo o contraseña incorrectas.", "OK");
            }
        }
        //==============================================================================

        //==============================================================================
        public async void Register(string name, string email, string phone, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Se requiere llenar todos los campos del formulario.", "OK");
            }
            else
            {
                if(password.Length < 8)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "La contraseña debe tener 8 caracteres minimo.", "OK");
                }
                else
                {
                    try
                    {
                        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
                        var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                        string gettoken = auth.FirebaseToken;
                        await AddUser(name, email, phone);
                        Login(email, password);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("EMAIL_EXIST"))
                        {
                            await Application.Current.MainPage.DisplayAlert("Error de cuenta", "Esta cuenta ya existe", "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                        }
                    }
                }
            }
        }
        //==============================================================================

        //==============================================================================
        public static async Task<string> SaveImage(Stream image, string filename)
        {
            var img = await firebaseStorage.Child("UsersImages").Child(filename).PutAsync(image);
            return img;
        }
        //==============================================================================

        //==============================================================================
        public void LogOut()
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            Preferences.Remove("Username");
            Preferences.Remove("Email");
            Preferences.Remove("Level");
            Preferences.Remove("Phone");
            Preferences.Remove("Gender");
            Preferences.Remove("Location");
            Preferences.Remove("Age");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
