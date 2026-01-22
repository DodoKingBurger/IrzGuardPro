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
            if (Hash_table.Exists("UniqueKey.config") && Guard.EqualsKey(Hash_table.GetString("UniqueKey.config"), Guard.Encrypt(DeviceSystem.GetCodeDevice())))
                return true;
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
        string str = Hash_table.mainDir;
        if (ExistsVerificationFile())
        {
            //App.Current.MainPage = new AppShell();
            //App.Current.MainPage = new NavigationPage(new AppShell());
            await Navigation.PushModalAsync(new MainPage());
        }           
        //#if WINDOWS
        //    timer_Load_Main.Stop();
        //#endif
    }

    #endregion

    #region Конструкторы 

    public SecurityPage()
	{
        InitializeComponent();
//        // Почему то на Windows не работает смена главной страницы сразу, а ток через время.
//#if WINDOWS
//        Task.Delay(1000);
////            timer_Load_Main.Tick += (s, e) => LoadPage();
////            timer_Load_Main.Start();
//#else
//        //        Task.Delay(1000);
//        //        LoadPage();
//#endif
        LoadPage();
    }

    #endregion
}
