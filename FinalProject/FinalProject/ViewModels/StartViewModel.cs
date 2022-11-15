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
    class StartViewModel
    {
        #region Attributes
        private string userName;

        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        #endregion
        //==============================================================================

        //==============================================================================
        #region Properties
        public string UserName
        {
            get { return this.userName; }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Commands
        public ICommand LogOutCommand
        {
            get
            {
                return new RelayCommand(LogOut);
            }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Methods

        public void LogOut()
        {
            UserProvider obj = new UserProvider();
            obj.LogOut();
        }
        #endregion
    }
}
