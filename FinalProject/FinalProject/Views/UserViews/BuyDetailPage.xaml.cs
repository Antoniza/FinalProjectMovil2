﻿using FinalProject.Models;
using FinalProject.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyDetailPage : ContentPage
    {
        CancellationTokenSource cts;
        public BuyDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ClientName.Text = Preferences.Get("Username", "");
            ClientPhone.Text = Preferences.Get("Phone", "");

            String list = Preferences.Get("ShoppingCar", "");
            var ShoppingList = JsonConvert.DeserializeObject<List<Shopping>>(list);

            Double total = 0.00;

            if (ShoppingList != null)
            {
                for (int i = 0; i < ShoppingList.Count(); i++)
                {
                    total += Convert.ToDouble(ShoppingList[i].TotalShop);
                }
            }

            TotalToPay.Text = total.ToString() + " Lps";

            if (Preferences.Get("Latitude", "") == "" || Preferences.Get("Longitude", "") == "")
            {
                GetCurrentLocationAndApply();
            }
            else
            {
                LoadLocation();
            }
        }

        private void CancelBuyButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async Task GetCurrentLocationAndApply()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    ClientLocation.Text = $" ({placemark.CountryCode}) {placemark.CountryName}, {placemark.AdminArea}, {placemark.Locality}";
                    Latitude.Text = $"{location.Latitude}";
                    Longitude.Text = $"{location.Longitude}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        async Task LoadLocation()
        {
            try
            {
                double latitude = Convert.ToDouble(Preferences.Get("Latitude", ""));
                double longitude = Convert.ToDouble(Preferences.Get("Longitude", ""));

                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                var placemark = placemarks?.FirstOrDefault();

                ClientLocation.Text = $" ({placemark.CountryCode}) {placemark.CountryName}, {placemark.AdminArea}, {placemark.Locality}";
                Latitude.Text = Preferences.Get("Latitude", "");
                Longitude.Text = Preferences.Get("Longitude", "");
            }
            catch (Exception ex)
            {
                await DisplayAlert("No ubicacionb", ex.Message, "OK");
            }
        }

        private async void MakeBuyButton_Clicked(object sender, EventArgs e)
        {
            string sendDetails = SendDetails.Text;
            string payWay = string.Empty;

            if (PayWayPicker.SelectedIndex != null && PayWayPicker.SelectedIndex >= 0)
            {
                payWay = PayWayPicker.Items[PayWayPicker.SelectedIndex];
                if (string.IsNullOrEmpty(sendDetails))
                {
                    await DisplayAlert("Alerta", "Se deben llenar todos los campos del formulario.", "OK");
                }
                else
                {
                    if(Preferences.Get("ShoppingCar","") == "")
                    {
                        await DisplayAlert("Error", "La lista de compras esta vacía.", "Entendido");
                    }
                    else
                    {
                        if(payWay == "Tarjeta (Proximamente)")
                        {
                            await DisplayAlert("Alerta", "El metodo de pago de tarjetas no ha sido implementado todavía.\nSeleccione otra forma de pago.", "Entendido");
                        }
                        else
                        {
                            Sale newSale = new Sale()
                            {
                                ClientName = ClientName.Text,
                                ClientMail = Preferences.Get("Email", ""),
                                ClientPhone = ClientPhone.Text,
                                ClientLatitude = Latitude.Text,
                                ClientLongitude = Longitude.Text,
                                Detail = SendDetails.Text,
                                TotalToPay = TotalToPay.Text,
                                PayFormat = payWay,
                                ShoppingCar = Preferences.Get("ShoppingCar", "")
                            };

                            bool res = await SalesProvider.AddSale(newSale);
                            Random rnd = new Random();
                            if (res)
                            {
                                await DisplayAlert("Satisfactorio", $"La compra ha sido enviada.\n\nTiempo estimado de llegada:\n{rnd.Next(25, 40)} Minutos.", "Entendido");
                                Preferences.Remove("ShoppingCar");
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Error", "No se puedo realizar la compra.", "Entendido");
                            }
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Se deben llenar todos los campos del formulario.", "OK");
            }
        }
    }
}