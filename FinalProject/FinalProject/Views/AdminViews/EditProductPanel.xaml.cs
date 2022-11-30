﻿using FinalProject.Models;
using FinalProject.Providers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductPanel : ContentPage
    {
        MediaFile file;
        Products product;
        bool ImageEdited;
        public EditProductPanel(Products _product)
        {
            InitializeComponent();
            AddProductImage.GestureRecognizers.Add(new TapGestureRecognizer((view) => TakeProductImage()));

            product = _product;

            FillForm();
        }

        private void FillForm()
        {
            ProductImage.Source = product.Image;
            NameProductText.Text = product.Name;
            PriceProductText.Text = product.Price;
            DescriptionProductText.Text = product.Description;

            CategoryPicker.Title = product.Category;
        }

        private async void TakeProductImage()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                ProductImage.Source = ImageSource.FromStream(() =>
                {
                    ImageEdited = true;
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            string CategoryProduct = string.Empty;
            string ImageProduct;

            if (CategoryPicker.SelectedIndex != null && CategoryPicker.SelectedIndex >= 0)
            {
                CategoryProduct = CategoryPicker.Items[CategoryPicker.SelectedIndex];
            }
            else
            {
                CategoryProduct = product.Category;
            }

            if (ImageEdited)
            {
                ImageProduct = await ProductProvider.SaveImage(file.GetStream(), Path.GetFileName(file.Path));
            }
            else
            {
                ImageProduct = product.Image;
            }

            Products dataUpdate = new Products()
            {
                Id = product.Id,
                Name = NameProductText.Text,
                Category = CategoryProduct,
                Image = ImageProduct,
                Description = DescriptionProductText.Text,
                Price = PriceProductText.Text

            };

            bool res = await ProductProvider.UpdateProduct(dataUpdate);

            if (res)
            {
                await Application.Current.MainPage.DisplayAlert("Satisfactorio", "Producto actualizado satisfactoriamente.", "OK");
                Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fallo de sistema", "No se pudo actualizar el producto.", "OK");
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void CancelButton_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}