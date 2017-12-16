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
        public int InvestmentAmount
        {
            get { return investmentAmount; }
            set
            {
                investmentAmount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InvestmentAmount)));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }
        public BitmapImage IconImage
        {
            get { return iconImage; }
            set
            {
                iconImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconImage)));
            }
        }
        public ICommand ImageSelectCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        int investmentAmount;
        string name;
        string address;
        string ImageFullName
        {
            get { return imageFullName; }
            set
            {
                imageFullName = value;

                if (!string.IsNullOrEmpty(imageFullName))
                {
                    var image = new BitmapImage();
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.BeginInit();
                    image.UriSource = new Uri(imageFullName, UriKind.Absolute);
                    image.EndInit();
                    IconImage = image;
                }
            }
        }
        string imageFullName;
        BitmapImage iconImage;
        SettingsXml xml;

        public ViewModel()
        {
            xml = new SettingsXml();

            if (xml.IsSuccess)
            {
                InvestmentAmount = xml.InvestmentAmount;
                Name = xml.Name;
                Address = xml.Address;
                ImageFullName = xml.ImageFullName;
            }

            SaveCommand = new DelegateCommand(() =>
            {
                xml.InvestmentAmount = InvestmentAmount;
                xml.Name = Name;
                xml.Address = Address;
                xml.ImageFullName = ImageFullName;
                xml.Update();

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
                        ImageFullName = movedFileFullName;
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