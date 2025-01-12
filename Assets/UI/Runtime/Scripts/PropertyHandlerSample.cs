using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.Runtime.Scripts
{
    public class PropertyHandlerSample : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _intValue;
        
        public int IntValue
        {
            get => _intValue;
            set => SetField(ref _intValue, value);
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
