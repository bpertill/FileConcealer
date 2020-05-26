using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Controls;

namespace FileConcealer
{
    public partial class DataViewModel : NotifyPropertyChandedBase
    {
        public DataViewModel()
        {
            _aes = new AESCryptoService();
            EncPaths = new ObservableCollection<string>();
            ButtonsEnabled = true;
            EncPaths.CollectionChanged += CollectionChanged;
        }
        private ObservableCollection<string> _encPaths;
        public ObservableCollection<string> EncPaths
        {
            get { return _encPaths; }
            set
            {
                _encPaths = value;
                OnPropertyChanged();
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(MaxProgress));
        }

        private Action NotifyStuff()
        {
            return null;
        }
        private int _progress;

        public int Progress
        {
            get { return _progress; }
            set { _progress = value; OnPropertyChanged(); }
        }

        public int MaxProgress
        {
            get { return EncPaths.Count; }
        }
        private bool _buttonsEnabled;

        public bool ButtonsEnabled
        {
            get { return _buttonsEnabled; }
            set { _buttonsEnabled = value; OnPropertyChanged(); }
        }

    }
}
