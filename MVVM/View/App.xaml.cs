using IrzGuardPro.MVVM.View;
using IrzGuardPro.Utility;

namespace IrzGuardPro
{
    public partial class App : Application
    {
        /// <summary>
        /// Проверка верификационного файла.
        /// </summary>
        /// <returns>True, если файл был найден и его содержимое совпадает с ключом.</returns>
        public bool ExistsVerificationFile()
        {
            try
            {
                if (File.Exists("UniqueKey.config"))
                {
                    if (RSAcrypt.EqualsKey(File.ReadAllText("UniqueKey.config")))
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
                return false;
            }
        }

        public App()
        {
            InitializeComponent();
            if (!ExistsVerificationFile()) 
            {
                MainPage = new AppSecurity(); 
            }
            else
            {
                MainPage = new AppShell();
            }
        }
    }
}
