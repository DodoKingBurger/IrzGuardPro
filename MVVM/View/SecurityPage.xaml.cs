using IrzGuardPro.MVVM.View;
using IrzGuardPro.MVVM.ViewModel;
using IrzGuardPro.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using IrzGuardPro;
using System.Threading.Tasks;
using System;

namespace IrzGuardPro.MVVM.View;

public partial class SecurityPage : ContentPage
{
    #region Методы

    /// <summary>
    /// Переход к страничке с реферальным кодом.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckingCodePass(object? sender, EventArgs e)
    {
        App.Current.MainPage = new AppShell();
    }

    #endregion

    #region Конструкторы 

    public SecurityPage()
	{
		InitializeComponent();
    }

    #endregion
    //        Title = "Accsess";
    //        Button backButton = new Button { Text = "Back", HorizontalOptions = LayoutOptions.Start };

    //        StringBuilder sb = new StringBuilder();

    //        sb.AppendLine($"Model:{DeviceInfo.Current.Model}");
    //        sb.AppendLine($"Manufacturer:{DeviceInfo.Current.Manufacturer}");
    //        sb.AppendLine($"Name:{DeviceInfo.Current.Name}");
    //        sb.AppendLine($"OS Version:{DeviceInfo.Current.VersionString}");
    //        sb.AppendLine($"Idiom:{DeviceInfo.Current.Idiom}");
    //        sb.AppendLine($"Platform:{DeviceInfo.Current.Platform}");


    //        bool isVirtual = DeviceInfo.Current.DeviceType switch
    //        {
    //            DeviceType.Physical => false,
    //            DeviceType.Virtual => true,
    //            _ => false,
    //        };


    //        string deviceID = "0000 0000 0000 0000";
    //#if ANDROID
    //             deviceID = Android.Provider.Settings.Secure.GetString(Platform.CurrentActivity.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

    //#elif IOS
    //            deviceID = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.ToString();
    //#elif WINDOWS
    //            deviceID = NetworkInterface.GetAllNetworkInterfaces()
    //                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
    //                .Select(nic => nic.GetPhysicalAddress().ToString())
    //                .FirstOrDefault();
    //#endif

    //        sb.AppendLine($"deviceID: {deviceID}");
    //        sb.AppendLine($"Virtual device? {isVirtual}");

    //        Label label = new Label { Text = sb.ToString() };

    // переход с обычной странницы назад
    //backButton.Clicked += async (o, e) => await Navigation.PopAsync(true);
}