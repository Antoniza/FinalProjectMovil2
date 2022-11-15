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
    public partial class UserForm : ContentPage
    {
        public UserForm()
        {
            InitializeComponent();

            UsersListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });

        }

        protected override async void OnAppearing()
        {
            var UsersList = await UserProvider.GetAllUser();
            UsersListView.ItemsSource = null;
            UsersListView.ItemsSource = UsersList;
            UsersListView.IsRefreshing = false;

        }

    }
}