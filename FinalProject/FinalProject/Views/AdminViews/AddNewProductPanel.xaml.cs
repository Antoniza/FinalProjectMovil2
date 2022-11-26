using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddNewProductPanel : ContentPage
    {
        MediaFile file;

        [Obsolete]
        public AddNewProductPanel()
        {
            InitializeComponent();

            AddProductImage.GestureRecognizers.Add(new TapGestureRecognizer((view) => TakeProductImage()));
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
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }

        }

        void AddNewProduct()
        {
            String NameProduct = NameProductText.Text;
            String PriceProduct = PriceProductText.Text;
            String DescriptionProduct = DescriptionProductText.Text;
            string Category = string.Empty;
            Category = CategoryPicker.Items[CategoryPicker.SelectedIndex];

            Application.Current.MainPage.DisplayAlert("Alerta", Category, "OK");
        }
    }
}