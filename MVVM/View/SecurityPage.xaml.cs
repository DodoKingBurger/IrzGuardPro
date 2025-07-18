using IrzGuardPro.MVVM.View;
using IrzGuardPro.MVVM.ViewModel;
using IrzGuardPro.Utility;
using IrzGuardPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System;

namespace IrzGuardPro;

/// <summary>
/// Страница для проверки доступа к приложению.
/// </summary>
public partial class SecurityPage : ContentPage
{
    #region Поля и свойства

    /// <summary>
    /// Таймер, для переключение страниц в случае прохождения успешной проверки.
    /// </summary>
    IDispatcherTimer timer_Load_Main = Application.Current.Dispatcher.CreateTimer();

    /// <summary>
    /// ViewModel.
    /// </summary>
    SecurityViewModel security = new SecurityViewModel();

    #endregion

    #region Методы

    /// <summary>
    /// Событие на ввод текста в текстовое поле. Изменить цвет текста после ввода неправильного пароля.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EntryBox_code_TextChanged(object sender, TextChangedEventArgs e)
   {
        if (sender is Entry entry && entry.TextColor != Colors.Black)
            entry.TextColor = Colors.Black;
    }

    /// <summary>
    /// Проверка верификации входного файла.
    /// </summary>
    /// <returns>True, если файл был найден и его содержимое совпадает с ключом.</returns>
    public bool ExistsVerificationFile()
    {
        try
        {
            if (Hash_table.Exists("UniqueKey.config"))
            {
                if (Guard.EqualsKey(Hash_table.GetString("UniqueKey.config"),Guard.Encrypt(DeviceSystem.GetCodeDevice())))
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "OK");
            return false;
        }
    }

    /// <summary>
    /// Загрузка страницы.
    /// </summary>
    private async void LoadPage()
    {
        security.Password = Guard.Encrypt(DeviceSystem.GetCodeDevice());
        // Не хотел через биндинг работать, иначе не работает. Не знаю почему :-(
        LabelCode.Text = security.Password;
        //
        if (ExistsVerificationFile())
        {
            App.Current.MainPage = new AppShell();
            //await Navigation.PushAsync(new MainPage());
        }           
        #if WINDOWS
            timer_Load_Main.Stop();
        #endif
    }

    #endregion

    #region Конструкторы 

    public SecurityPage()
	{
        Loaded += (s, e) => LoadPage();
        BindingContext = security;
        InitializeComponent();
        timer_Load_Main.Interval = TimeSpan.FromSeconds(2);
        // Почему то на Windows не работает смена главной страницы сразу, а ток через время.
#if WINDOWS
            timer_Load_Main.Tick += (s, e) => LoadPage();
               timer_Load_Main.Start();
#else
        Task.Delay(1000);
        LoadPage();
#endif
    }

    #endregion

    #region NOT USED

    /// <summary>
    /// Переход к страничке с реферальным кодом.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //private void CheckingCodePass(object? sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Guard.EqualsKey(EntryBox_code.Text))
    //        {
    //            if (!Hash_table.Exists("UniqueKey.config"))
    //                Hash_table.CreateFile("UniqueKey.config");
    //            Hash_table.SetString("UniqueKey.config", Guard.Encrypt(DeviceSystem.GetCodeDevice()));
    //            App.Current.MainPage = new AppShell();
    //        }
    //        else
    //        {
    //            EntryBox_code.TextColor = Colors.Red;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        DisplayAlert("Ошибка",ex.Message,"OK");
    //    }
    //}
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
    #endregion

}
