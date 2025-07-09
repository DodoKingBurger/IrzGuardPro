using IrzGuardPro.MVVM.Model;
using IrzGuardPro.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.MVVM.ViewModel
{
    public class InvateViewModel : INotifyPropertyChanged
    {
        public List<string> ListLevelAccess = ["Электромонтер", "Мастер", "Администратор"];

        PasswordResources resources = new PasswordResources();

        public event PropertyChangedEventHandler? PropertyChanged;

        public DateTime DateTime_Create
        {
            get => this.resources.DateTimeCreated;
            set
            {
                if (!value.Equals(null) && !this.resources.DateTimeCreated.Equals(value))
                {
                    this.resources.DateTimeCreated = value;
                    Invate_str = $"{value.ToString()}";
                }
            }
        }
        public int Password
        {
            get => this.resources.Password;
            set
            {
                if (int.IsPositive(value) && !this.resources.Password.Equals(value))
                {
                    this.resources.Password = value;
                    Invate_str = $"{value}";
                }
            }
        }

        private string invate_str = "00:00:00 - ---";

        public string Invate_str 
        {
            get => this.invate_str ;
            set 
            {
                if (!string.IsNullOrEmpty(value) && !invate_str.Equals(value))
                {
                    invate_str = $"{this.resources.DateTimeCreated.Hour}:" +
                        $"{string.Format("{0:d2}", this.resources.DateTimeCreated.Minute)}:" +
                        $"{string.Format("{0:d2}", this.resources.DateTimeCreated.Second)} - " +
                        $"{string.Format("{0:d3}", this.resources.Password)}";
                    OnPropertyChanged();
                }
            }
        }


        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
