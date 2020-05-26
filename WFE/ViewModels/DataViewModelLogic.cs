using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileConcealer
{
    public partial class DataViewModel : NotifyPropertyChandedBase
    {
        public readonly AESCryptoService _aes;


        public async  void EncryptOrDecrypt(Mode mode, string password)
        {
            ConcurrentQueue<Exception> exceptions = null;

            Progress = 0;
            ButtonsEnabled = false;

            await Task.Run(() =>
            {
                exceptions = DoAesParallel(mode, password);
            });

            EncPaths.Clear();
            ButtonsEnabled = true;

            ExceptionHandling(exceptions);
        }

        private ConcurrentQueue<Exception> DoAesParallel(Mode mode, string password)
        {
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(EncPaths, (filename) =>
            {
                try
                {
                    if (mode == Mode.Encrypt)
                        _aes.FileEncrypt(filename, password);

                    else if (mode == Mode.Decrypt)
                        _aes.FileDecrypt(filename, $"{filename.Substring(0, filename.Length - ".aes".Length)}", password);

                    Progress++;
                }
                catch (IOException ex)
                {
                    exceptions.Enqueue(ex);
                }
            });
            return exceptions;
        }
        public void ExceptionHandling(ConcurrentQueue<Exception> exceptions)
        {
            if (exceptions != null && exceptions.Count > 0)
            {
                string message = string.Join("\n", exceptions.Select(ex => ex.Message));
                MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                exceptions.Clear();
            }
        }
    }
}
