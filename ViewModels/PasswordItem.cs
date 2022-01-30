using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPasswordGenerator.BaseModels;

namespace WPFPasswordGenerator.ViewModels
{
    public class PasswordItem : INotifyPropertyChangedBase
    {
        public int Id { get; set; }
        public string PasswordItemValue { get; set; }
    }
}
