using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace XPPassbook
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand ExitCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            ExitCommand = new DelegateCommand(() => Application.Current.MainWindow.Close() );
        }
    }
}