using IrzGuardPro.MVVM.Model;
using IrzGuardPro.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IrzGuardPro
{
    /// <summary>
    /// ViewModel для страницы проверки безопасности.
    /// </summary>
    public class SecurityViewModel : INotifyPropertyChanged
    {
        #region Поля и свойства

        /// <summary>
        /// Событие по изменению свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        
        /// <summary>
        /// Модель.
        /// </summary>
        public SecurityModel model = new SecurityModel();

        /// <summary>
        /// Комманда для проверки допуска.
        /// </summary>
        public ICommand CheckAllowingCommand {  get; set; }

        /// <summary>
        /// Код доступа.
        /// </summary>
        public string Password
        {
            get => this.model.Code;
            set
            {
                if (!string.IsNullOrEmpty(value) && !this.model.Code.Equals(value))
                {
                    this.model.Code = string.Format("{0:d4}",value);
                    OnPropertyChanged();
                }
            }
        }

        private string enteredCode = "";

        public string EnteredCode 
        {
            get => this.enteredCode;
            set 
            {
                if (!string.IsNullOrEmpty(value) && !this.model.Code.Equals(value))
                {
                    this.enteredCode = value;
                    //OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Базовый класс

        /// <summary>
        /// Функция для евента по изменнению свойства.
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)this.CheckAllowingCommand).ChangeCanExecute(); 
        }

        #endregion

        #region Констуркторы

        public SecurityViewModel() 
        {
            //Проверка допуска через файл.
            this.CheckAllowingCommand = new Command((object? args) =>
            {
                if (Guard.EqualsKey(this.enteredCode))
                {
                    if (!Hash_table.Exists("UniqueKey.config"))
                        Hash_table.CreateFile("UniqueKey.config");
                    Hash_table.SetString("UniqueKey.config", Guard.Encrypt(DeviceSystem.GetCodeDevice()));
                    App.Current.MainPage = new AppShell();
                }
                else
                {
                    if (args is Entry entry) entry.TextColor = Colors.Red;
                }
            });
        }

        #endregion
    }
}
