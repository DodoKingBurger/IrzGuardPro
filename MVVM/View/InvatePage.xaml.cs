using System;
using Microsoft.Maui.Controls;
using IrzGuardPro;
using IrzGuardPro.MVVM.ViewModel;
using IrzGuardPro.Utility;

namespace IrzGuardPro.MVVM.View;

public partial class InvatePage : ContentPage
{
    #region Поля и свойства

    /// <summary>
    /// ViewModel ну тип косячный, но вариант Модель он не видит значит работает ))))
    /// </summary>
    public InvateViewModel labelViewModel = new InvateViewModel();

    /// <summary>
    /// Таймер для обновления времени.
    /// </summary>
    IDispatcherTimer timer_time = Application.Current.Dispatcher.CreateTimer();

    /// <summary>
    /// Таймер для обновления пароля.
    /// </summary>
    IDispatcherTimer timer_password = Application.Current.Dispatcher.CreateTimer();

    #endregion

    #region Методы

    /// <summary>
    /// Событие по выбору в combobox какого-то уровня доступа.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ComboBox_AccessLevel_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (sender is Picker) 
        {
            Hash_table.SetInt("ConfigFile.txt", ((Picker)sender).SelectedIndex);
            DateTime dateTime = DateTime.Now;
            //timer_password.Interval = TimeSpan.FromMinutes(10 - (dateTime.Minute % 10) - (dateTime.Second / 100));            
            labelViewModel.Password = Guard.GenerateReferenceCode(((Picker)sender).SelectedIndex, dateTime);
        }
    }


    private async void ToBackMainPage (object? sender, EventArgs e) 
    {
        await Navigation.PopAsync(true);
    }

    /// <summary>
    /// Событие тик таймера. Визуальное отображения кода и времени на экран устройства.
    /// К соэалению через загрузку XAML кода.
    /// </summary>
    private void ViewReferenceCode_time()
    {
        DateTime dateTime = DateTime.Now;
        //MainThread.BeginInvokeOnMainThread(() =>
        //{            
            labelViewModel.DateTime_Create = dateTime;
        //});
    }

    private void ViewReferenceCode_code()
    {
        int save_LevelAccess = Hash_table.GetInt("ConfigFile.txt");
        DateTime dateTime = DateTime.Now;
        //MainThread.BeginInvokeOnMainThread(() =>
        //{
            labelViewModel.Password = Guard.GenerateReferenceCode(save_LevelAccess, dateTime);
        //});
        timer_password.Interval = TimeSpan.FromMinutes(10);
    }

    public void LoadPage() 
    {
        BindingContext = labelViewModel;
        ComboBox_AccessLevel.ItemsSource = labelViewModel.ListLevelAccess;
        int save_LevelAccess = Hash_table.GetInt("ConfigFile.txt");
        ComboBox_AccessLevel.SelectedIndex = save_LevelAccess;
        DateTime dateTime = DateTime.Now;
        labelViewModel.Password = Guard.GenerateReferenceCode(save_LevelAccess, dateTime);

        timer_password.Interval = TimeSpan.FromSeconds((10 - (dateTime.Minute % 10)) * 60  - dateTime.Second);
        timer_time.Interval = TimeSpan.FromSeconds(1);

        timer_password.Tick += (s, e) => ViewReferenceCode_code();
        timer_time.Tick += (s, e) => ViewReferenceCode_time();

        timer_time.Start();
        timer_password.Start();
    }

    #endregion

    #region Конструкторы

    public InvatePage()
	{
        InitializeComponent();
        LoadPage();
    }

    //public InvatePage()
    //{
    //    Title = "Invate";
    //    Button backButton = new Button { Text = "Back", HorizontalOptions = LayoutOptions.Start };
    //    Label label = new Label { Text = "InvatePage" };

    //    // переход с обычной странницы назад
    //    backButton.Clicked += async (o, e) => await Navigation.PopAsync(true);
    //    Content = new StackLayout { Children = { label, backButton } };
    //}

    #endregion
}