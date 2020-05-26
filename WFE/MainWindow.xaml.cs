using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace FileConcealer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataViewModel DataModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataModel = new DataViewModel();
            DataContext = DataModel;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DataModel.EncPaths.Clear();
        }

        private void Enctypt_Click(object sender, RoutedEventArgs e)
        {
            DataModel.EncryptOrDecrypt(Mode.Encrypt, PwBox.Password);
        }


        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            DataModel.EncryptOrDecrypt(Mode.Decrypt, PwBox.Password);
        }

        private void LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Multiselect = true };
            if (openFileDialog.ShowDialog() == true)
                foreach (var item in openFileDialog.FileNames)
                {
                    DataModel.EncPaths.Add(item);
                }
        }
    }
}
