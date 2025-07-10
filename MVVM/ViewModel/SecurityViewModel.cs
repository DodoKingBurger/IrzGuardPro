using IrzGuardPro.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel для страницы проверки безопасности.
    /// </summary>
    public class SecurityViewModel : INotifyPropertyChanged
    {        
        /// <summary>
        /// Событие по изменению свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        
        /// <summary>
        /// Модель.
        /// </summary>
        public SecurityModel model = new SecurityModel();


        private string password;
        /// <summary>
        /// Код доступа.
        /// </summary>
        public string Password
        {
            get => this.password;
            set
            {
                if (!string.IsNullOrEmpty(value) && !this.model.Code.Equals(value))
                {
                    this.model.Code = string.Format("{0:d4}",value);
                    this.password = this.model.Code;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Функция для евента по изменнению свойства.
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
