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
    class PasswordViewModel : BaseViewModel
    {
        #region Attributes
        private string email;

        private string WebApiKey = "AIzaSyD4MtmT8v3oPcO655V3P0hZ2wRwVcUyZvU";
        #endregion
        //==============================================================================

        //==============================================================================
        #region Properties
        public string PasswordEmailText
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Commands
        public ICommand RecoveryCommand
        {
            get
            {
                return new RelayCommand(SendMail);
            }
        }
        #endregion
        //==============================================================================

        //==============================================================================
        #region Methods
        public async void SendMail()
        {
            if (string.IsNullOrEmpty(email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tienes que llenar los campos del formulario.", "OK");
            }
            else
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));

                try
                {
                    await authProvider.SendPasswordResetEmailAsync(email);
                    await Application.Current.MainPage.DisplayAlert("Enviado", "Se le ha enviado un correo para restablecer su contraseña. Si no lo encuentra, revise la carpeta de Spam", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                catch (Exception e)
                {
                }
            }
        }
        #endregion
    }
}
