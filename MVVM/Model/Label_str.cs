using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.MVVM.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Label_str
    {
        /// <summary>
        /// 
        /// </summary>
        public string Past { get; set; } = "00:00 - ---";
        
        /// <summary>
        /// 
        /// </summary>
        public string Now { get; set; } = "00:00:00 - ---";
        
        /// <summary>
        /// Строка в будуйщие
        /// </summary>
        public string Future { get; set; } = "00:00 - ---";
    }
}
