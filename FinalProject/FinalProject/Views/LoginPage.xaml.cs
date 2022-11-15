using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        [Obsolete]
        public LoginPage()
        {
            InitializeComponent();
            GoToRegister.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToRegister()));
            ForgotPassword.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToRecover()));

            BindingContext = new LoginViewModel();
        }

        void ToRegister()
        {
            Navigation.PushAsync(new RegisterPage());
        }

        void ToRecover()
        {
            Navigation.PushAsync(new PasswordPage());
        }
    }
}