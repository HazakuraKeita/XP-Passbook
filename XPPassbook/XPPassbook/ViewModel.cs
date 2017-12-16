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
using System.Windows.Threading;
using XPPassbook.Models;

namespace XPPassbook
{
    public class ViewModel : INotifyPropertyChanged
    {
        public XPer XPer { get; set; }
        public Visibility LoadingMessageVisibility
        {
            get { return loadingMessageVisibility; }
            set
            {
                loadingMessageVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingMessageVisibility)));
            }
        }
        public ICommand LoadedCommand { get; private set; }
        public ICommand ImageSelectCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        Visibility loadingMessageVisibility;
        DispatcherTimer loading;

        public ViewModel()
        {
            XPer = new XPer();
            LoadedCommand = new DelegateCommand(Update);
            UpdateCommand = new DelegateCommand(Update);

            SaveCommand = new DelegateCommand((o) =>
            {
                XPer.Save();
                Update();
            });

            ImageSelectCommand = new DelegateCommand((o) =>
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

            ExitCommand = new DelegateCommand((o) => System.Windows.Application.Current.MainWindow.Close() );

            loading = new DispatcherTimer();
            loading.Interval = new TimeSpan(0, 0, 1);
            loading.Tick += (o, e) =>
            {
                if (LoadingMessageVisibility == Visibility.Collapsed)
                {
                    LoadingMessageVisibility = Visibility.Visible;
                }
                else
                {
                    LoadingMessageVisibility = Visibility.Collapsed;
                }
            };
        }

        private async void Update(object parameter = null)
        {
            loading.Start();
            await XPer.Load();
            loading.Stop();

            LoadingMessageVisibility = Visibility.Collapsed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}