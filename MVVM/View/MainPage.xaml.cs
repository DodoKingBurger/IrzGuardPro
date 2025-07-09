using IrzGuardPro.MVVM.View;
using IrzGuardPro.MVVM.ViewModel;
using IrzGuardPro.Utility;
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
        /// ViewModel ну тип косячный, но вариант Модель он не видит значит работает ))))
        /// </summary>
        public MainViewModel labelViewModel = new MainViewModel();

        #endregion

        #region Методы

        /// <summary>
        /// Событие тик таймера. Визуальное отображения кода и времени на экран устройства.
        /// К соэалению через загрузку XAML кода.
        /// </summary>
        private void ViewCodePass() 
        {
            //MainThread.BeginInvokeOnMainThread(() =>
            //{
            try
            {
                int save_LevelAccess = Hash_table.GetInt("ConfigFile.txt");
                DateTime dateTime = DateTime.Now;
                DateTime dateTime_Past = dateTime.AddHours(-1);
                DateTime dateTime_Future = dateTime.AddHours(1);

                labelViewModel.Name_Future = $"{dateTime_Future.Hour}:00 - " +
                        $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime_Future))}";
                labelViewModel.Name_Past = $"{dateTime_Past.Hour}:00 - " +
                        $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime_Past))}";
                labelViewModel.Name_Now = $"{dateTime.Hour}:{string.Format("{0:d2}", dateTime.Minute)}:{string.Format("{0:d2}", dateTime.Second)} - " +
                    $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime))}";
            }
            catch (Exception ex) 
            {
                DisplayAlert("Ошибка", $"{ex.Message}\n Продолжить ?", "Yes","No");
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

            //});
        }

        /// <summary>
        /// Переход к информации об устройстве.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void ToInfoDevicePage(object? sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new SecurityPage());
        //}

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
            if (sender is Picker)
                Hash_table.SetInt("ConfigFile.txt", ((Picker)sender).SelectedIndex);
        }

        /// <summary>
        /// Загрузка страницы.
        /// </summary>
        private void LoadPage() 
        {
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

        #region Констуркторы 

        public MainPage()
        {
            InitializeComponent();
            LoadPage();
        }

        #endregion
    }

}
