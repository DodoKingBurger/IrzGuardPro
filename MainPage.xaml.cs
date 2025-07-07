
using System.Security.AccessControl;

namespace IrzGuardPro
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private async void ToInfoDevicePage(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoDevicePage());
        }

        private async void ToCommonPage(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommonPage());
        }
    }

}
