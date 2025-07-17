using IrzGuardPro.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro
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
