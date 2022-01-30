using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFPasswordGenerator.BaseModels
{
    public abstract class INotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}