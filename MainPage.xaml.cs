
using IrzGuardPro.Utility;
using System;
using System.Security.AccessControl;

namespace IrzGuardPro
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        IDispatcherTimer timer_hour = Application.Current.Dispatcher.CreateTimer();
        IDispatcherTimer timer_minute = Application.Current.Dispatcher.CreateTimer();

        public MainPage()
        {
            InitializeComponent();


            timer_minute.Interval = TimeSpan.FromSeconds(1);
            //timer_hour.Interval = TimeSpan.FromHours(1);

            timer_minute.Tick += (s, e) => ViewCodePass();
            timer_minute.Start();
            //ViewCodePass();
        }

        private async void ToInfoDevicePage(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoDevicePage());
        }

        private async void ToInvateBtnPage(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new InvatePage());
        }

        private void ComboBox_AccessLevel_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Hash_table.SetInt("ConfigFile.txt",ComboBox_AccessLevel.SelectedIndex);
            //ViewCodePass();
        }

        private void ViewCodePass() 
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DateTime dateTime = DateTime.Now;
                DateTime dateTime_Past = dateTime.AddHours(-1);
                DateTime dateTime_Future = dateTime.AddHours(1);
                int save_LevelAccess = Hash_table.GetInt("ConfigFile.txt");
                ComboBox_AccessLevel.SelectedIndex = save_LevelAccess;

                LabelCodePassPast.LoadFromXaml($"<Label Text =\"{dateTime_Past.Hour}:00 - " +
                    $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime_Past))} \" FontSize =\"28\"/>");
                LabelCodePass.LoadFromXaml($"<Label Text =\"{dateTime.Hour}:{string.Format("{0:d2}", dateTime.Minute)}:{string.Format("{0:d2}",dateTime.Second)} - " +
                    $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime))} \" FontSize =\"35\" TextColor=\"Green\"/>");
                LabelCodePassFuture.LoadFromXaml($"<Label Text =\"{dateTime_Future.Hour}:00 - " +
                    $"{string.Format("{0:d3}", Guard.GeneratePass(save_LevelAccess, dateTime_Future))} \" FontSize =\"28\"/>");
            });
        }
    }

}
