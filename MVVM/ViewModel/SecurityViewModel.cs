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
    public class SecurityViewModel : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public SecurityModel model = new SecurityModel();

        public string Password
        {
            get => this.model.Code;
            set
            {
                if (!string.IsNullOrEmpty(value) && !this.model.Code.Equals(value))
                {
                    this.model.Code = value;
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
