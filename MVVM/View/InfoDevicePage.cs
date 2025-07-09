
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.MVVM.View
{
    class InfoDevicePage : ContentPage
    {
        public InfoDevicePage() 
        {
            Title = "Accsess";
            Button backButton = new Button { Text = "Back", HorizontalOptions = LayoutOptions.Start };

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Model:{DeviceInfo.Current.Model}");
            sb.AppendLine($"Manufacturer:{DeviceInfo.Current.Manufacturer}");
            sb.AppendLine($"Name:{DeviceInfo.Current.Name}");
            sb.AppendLine($"OS Version:{DeviceInfo.Current.VersionString}");
            sb.AppendLine($"Idiom:{DeviceInfo.Current.Idiom}");
            sb.AppendLine($"Platform:{DeviceInfo.Current.Platform}");

            bool isVirtual = DeviceInfo.Current.DeviceType switch
            {
                DeviceType.Physical => false,
                DeviceType.Virtual => true,
                _ => false,
            };


            string deviceID = "";
            
        #if ANDROID
             deviceID = Android.Provider.Settings.Secure.GetString(Platform.CurrentActivity.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

        #elif IOS
            deviceID = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        #endif

            sb.AppendLine($"Virtual device? {isVirtual}");

            Label label = new Label { Text = sb.ToString() };

            // переход с обычной странницы назад
            backButton.Clicked += async (o, e) => await Navigation.PopAsync(true);
            Content = new StackLayout { Children = { label, backButton } };
        }
    }
}
