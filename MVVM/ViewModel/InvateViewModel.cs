using IrzGuardPro.MVVM.Model;
using IrzGuardPro.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel для страницы приглашения коллеги.
    /// </summary>
    public class InvateViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Уровни доступа.
        /// </summary>
        public List<string> ListLevelAccess = ["Электромонтер", "Мастер", "Администратор"];

        /// <summary>
        /// Рессурсы для генерации пароля.
        /// </summary>
        CodeResources resources = new CodeResources();

        /// <summary>
        /// Event по изменению свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Время создания кода доступа.
        /// </summary>
        public DateTime DateTime_Create
        {
            get => this.resources.DateTimeCreated;
            set
            {
                if (!value.Equals(null) && !this.resources.DateTimeCreated.Equals(value))
                {
                    this.resources.DateTimeCreated = value;
                    int save_LevelAccess = Hash_table.GetInt("ConfigFile.txt");
                    this.Password = Guard.GenerateReferenceCode(save_LevelAccess, this.resources.DateTimeCreated);
                    Invate_str = $"{value.ToString()}";
                }
            }
        }

        /// <summary>
        /// Код доступа.
        /// </summary>
        public int Password
        {
            get => this.resources.Code;
            set
            {
                if (int.IsPositive(value) && !this.resources.Code.Equals(value))
                {
                    this.resources.Code = value;
                    Invate_str = $"{value}";
                }
            }
        }

        private string invate_str = "00:00:00 - ---";

        /// <summary>
        /// Строка для вывода на экран.
        /// </summary>
        public string Invate_str 
        {
            get => this.invate_str ;
            set 
            {
                if (!string.IsNullOrEmpty(value) && !invate_str.Equals(value))
                {
                    this.invate_str = $"{this.resources.DateTimeCreated.Hour}:" +
                        $"{string.Format("{0:d2}", this.resources.DateTimeCreated.Minute)}:" +
                        $"{string.Format("{0:d2}", this.resources.DateTimeCreated.Second)} - " +
                        $"{string.Format("{0:d3}", this.resources.Code)}";
                    OnPropertyChanged();
                }
            }
        }

        public string Version
        {
            get => $"ИРЗ ТЕК: {Assembly.GetExecutingAssembly().GetName().Version}";
        }

        /// <summary>
        /// Изменение свойства.
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
