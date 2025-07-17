using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.MVVM.Model
{
    /// <summary>
    /// Модель для хранения кода доступа к программе.
    /// </summary>
    public class SecurityModel
    {
        /// <summary>
        /// Четырехзначный код доступа к программе IrzGuardPro
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
