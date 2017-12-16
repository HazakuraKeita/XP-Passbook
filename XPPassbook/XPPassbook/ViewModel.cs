using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using XPPassbook.Models;

namespace XPPassbook
{
    public class ViewModel : INotifyPropertyChanged
    {
        public XPer XPer { get; set; }
        public ICommand LoadedCommand { get; private set; }
        public ICommand ImageSelectCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        BitmapImage iconImage;

        public ViewModel()
        {
            XPer = new XPer();

            LoadedCommand = new DelegateCommand(async () =>
            {
                await XPer.Load();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            });

            SaveCommand = new DelegateCommand(() =>
            {
                XPer.Save();
                Update();
            });

            ImageSelectCommand = new DelegateCommand(() =>
            {
                var dialog = new OpenFileDialog();
                var result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/XpJp/XP-Passbook/";
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        var movedFileFullName = directory + Path.GetFileName(dialog.FileName);
                        File.Copy(dialog.FileName, movedFileFullName);
                        XPer.ImageFullName = movedFileFullName;
                    }
                    catch { }
                }
            });

            ExitCommand = new DelegateCommand(() => System.Windows.Application.Current.MainWindow.Close() );
        }

        private void Update(object parameter = null)
        {

        }
    }
}