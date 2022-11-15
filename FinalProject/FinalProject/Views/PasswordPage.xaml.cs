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
    public partial class PasswordPage : ContentPage
    {
        [Obsolete]
        public PasswordPage()
        {
            InitializeComponent();
            BindingContext = new PasswordViewModel();

            GoToLogin.GestureRecognizers.Add(new TapGestureRecognizer((view) => ToLogin()));
        }

        void ToLogin()
        {
            Navigation.PopAsync();
        }
    }
}