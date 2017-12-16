using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        public string ImageFullName
        {
            get { return imageFullName; }
            set
            {
                imageFullName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageFullName)));
            }
        }
        public ICommand ExitCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        int investmentAmount;
        string name;
        string address;
        string imageFullName;

        public ViewModel()
        {
            ExitCommand = new DelegateCommand(() => Application.Current.MainWindow.Close() );
        }
    }
}