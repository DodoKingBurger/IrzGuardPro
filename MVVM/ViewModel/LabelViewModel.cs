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
    public class LabelViewModel : INotifyPropertyChanged
    {
        Label_str label = new Label_str { Now = "00:00:00 - ---",
            Past = "00:00:00 - ---",
            Future = "00:00:00 - ---"
        };
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public string Name_Past 
        {
            get => label.Past; 
            set
            {
                if (!string.IsNullOrEmpty(value) && label.Past != value)
                {
                    label.Past = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name_Now
        {
            get => label.Now;
            set
            {
                if (!string.IsNullOrEmpty(value) && label.Now != value)
                {
                    label.Now = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name_Future
        {
            get => label.Future;
            set
            {
                if (!string.IsNullOrEmpty(value) && label.Future != value)
                {
                    label.Future = value;
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
