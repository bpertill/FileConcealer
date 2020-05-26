using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileConcealer
{
    public class NotifyPropertyChandedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
