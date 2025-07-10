using IrzGuardPro.MVVM.View;
using IrzGuardPro.Utility;

namespace IrzGuardPro
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppSecurity(); 
        }
    }
}
