using FinalProject.Providers;
using FinalProject.Views;
using Firebase.Auth;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string email;
        private string password;
        #endregion
        //==============================================================================

        //==============================================================================
        #region Properties
        public string LoginEmailText
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string LoginPasswordText
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Metheods
        private async void Login()
        {
            UserProvider obj = new UserProvider();
            obj.Login(email, password);
        }
        #endregion
    }
}
