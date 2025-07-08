using IrzGuardPro.Page;
using IrzGuardPro.Utility;
using IrzGuardPro.Utility.MVVM.ViewModel;
using System;
using System.Security.AccessControl;

namespace IrzGuardPro
{
    public partial class MainPage : ContentPage
    {
        #region Поля и свойства

        /// <summary>
        /// Таймер.
        /// </summary>
        IDispatcherTimer timer_minute = Application.Current.Dispatcher.CreateTimer();
        //IDispatcherTimer timer_hour = Application.Current.Dispatcher.CreateTimer();

        /// <summary>
        /// Время -1 час от нынешнего времни.
        /// </summary>
        DateTime dateTime_Past = new();

        /// <summary>
        /// Время +1 час от нынешнего времни.
        /// </summary>
        DateTime dateTime_Future = new();

        /// <summary>
        /// ViewModel ну тип косячный, но вариант Модель он не видит значит работает ))))
        /// </summary>
        public LabelViewModel labelViewModel = new LabelViewModel();

        #endregion

        #region Методы

        /// <summary>
        /// Событие тик таймера. Визуальное отображения кода и времени на экран устройства.
        /// К соэалению через загрузку XAML кода.
        /// </summary>
        private void ViewCodePass() 
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DateTime dateTime = DateTime.Now;

                int save_LevelAccess = Hash_table.GetInt("ConfigFile.txt");

                labelViewModel.Name_Now = $"{dateTime.Hour}:{string.Format("{0:d2}", dateTime.Minute)}:{string.Format("{0:d2}", dateTime.Second)} - " +
                    $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime))}";

                if (this.dateTime_Future != dateTime.AddHours(1))
                {
                    this.dateTime_Future = dateTime.AddHours(1);
                    labelViewModel.Name_Future = $"{this.dateTime_Future.Hour}:00 - " +
                        $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, this.dateTime_Future))}";
                }

                if (this.dateTime_Past != dateTime.AddHours(-1)) 
                {
                    this.dateTime_Past = dateTime.AddHours(-1);
                    labelViewModel.Name_Past = $"{this.dateTime_Past.Hour}:00 - " +
                        $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, this.dateTime_Past))}";
                }

                /*if(this.dateTime_Future != dateTime.AddHours(1)) 
                {
                    this.dateTime_Future = dateTime.AddHours(1);
                    //Почему то XAML не хочет видеть VM через binding пришлось вручную работать через загрузку XAML кода.
                    LabelCodePassPast.LoadFromXaml($"<Label Text =\"{this.dateTime_Past.Hour}:00 - " +
                        $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, this.dateTime_Past))} \"/>");
                }

                if (this.dateTime_Past != dateTime.AddHours(-1)) 
                {
                    this.dateTime_Past = dateTime.AddHours(-1);
                    //Почему то XAML не хочет видеть VM через binding пришлось вручную работать через загрузку XAML кода.
                    LabelCodePassFuture.LoadFromXaml($"<Label Text =\"{this.dateTime_Future.Hour}:00 - " +
                        $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, this.dateTime_Future))} \" />");
                }

                //Почему то XAML не хочет видеть VM через binding пришлось вручную работать через загрузку XAML кода.
                LabelCodePass.LoadFromXaml($"<Label Text =\"{dateTime.Hour}:{string.Format("{0:d2}", dateTime.Minute)}:{string.Format("{0:d2}",dateTime.Second)} - " +
                    $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime))} \" />");*/

            });
        }

        /// <summary>
        /// Переход к информации об устройстве.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToInfoDevicePage(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoDevicePage());
        }

        /// <summary>
        /// Переход к страничке с реферальным кодом.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToInvateBtnPage(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new InvatePage());
        }

        /// <summary>
        /// Событие по выбору в combobox какого-то уровня доступа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_AccessLevel_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Hash_table.SetInt("ConfigFile.txt", ComboBox_AccessLevel.SelectedIndex);
        }

        #endregion

        #region Констуркторы 

        public MainPage()
        {
            InitializeComponent();

            BindingContext = labelViewModel;
            ComboBox_AccessLevel.ItemsSource = labelViewModel.ListLevelAccess;
            ComboBox_AccessLevel.SelectedIndex = Hash_table.GetInt("ConfigFile.txt");

            timer_minute.Interval = TimeSpan.FromSeconds(1);
            //timer_hour.Interval = TimeSpan.FromHours(1);

            timer_minute.Tick += (s, e) => ViewCodePass();
            timer_minute.Start();
            //ViewCodePass();
        }

        #endregion
    }

}
