using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.Diagnostics;

namespace WFE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _encPaths;
        public ObservableCollection<string> EncPaths
        {
            get { return _encPaths; }
            set { _encPaths = value; }
        }
        AESCryptoService _aes;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _aes = new AESCryptoService();
            EncPaths = new ObservableCollection<string>();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            EncPaths.Clear();
        }

        private void Enctypt_Click(object sender, RoutedEventArgs e)
        {
            EncDecrypt(Mode.Encrypt);
        }


        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            EncDecrypt(Mode.Decrypt);
        }

        private async void EncDecrypt(Mode mode)
        {
           
            ProgressB.Maximum = EncPaths.Count;
            ProgressB.Value = 0;
            await Task.Run(() =>
            {
                Parallel.ForEach(EncPaths, (filename) =>
                {
                    if (mode == Mode.Encrypt)
                        _aes.FileEncrypt(filename, PwBox.Password);

                    else if (mode == Mode.Decrypt)
                        _aes.FileDecrypt(filename, $"{filename.Substring(0, filename.Length - ".aes".Length)}", PwBox.Password);
                    Dispatcher.Invoke(() =>
                    {
                        ProgressB.Value++;
                    });
                });
                 });
            EncPaths.Clear();
        }
        public enum Mode
        {
            Encrypt,
            Decrypt
        }

        private void LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Multiselect = true };
            if (openFileDialog.ShowDialog() == true)
                foreach (var item in openFileDialog.FileNames)
                {
                    EncPaths.Add(item);
                }
        }
    }
}
