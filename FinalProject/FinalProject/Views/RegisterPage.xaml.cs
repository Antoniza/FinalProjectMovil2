using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        [Obsolete]
        public RegisterPage()
        {
            InitializeComponent();
            GoToLogin.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToLogin()));

            BindingContext = new RegisterViewModel();
        }

        void ToLogin()
        {
            Navigation.PopAsync();
        }
    }
}