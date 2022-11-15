using FinalProject.Models;
using FinalProject.Providers;
using FinalProject.Views;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        #region Attributes
        private string name;
        private string email;
        private string phone;
        private string password;
        #endregion
        //==============================================================================

        //==============================================================================
        #region Properties
        public string RegisterNameText
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }
        public string RegisterEmailText
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string RegisterPhoneText
        {
            get { return this.phone; }
            set { SetValue(ref this.phone, value); }
        }
        public string RegisterPasswordText
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Functions

        
        #endregion
        //==============================================================================

        //==============================================================================
        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }
        #endregion

        #region Metheods
        private async void Register()
        {
            UserProvider obj = new UserProvider();
            obj.Register(name, email, phone, password);
        }
        #endregion
    }
}
