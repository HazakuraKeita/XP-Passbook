﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XPPassbook
{
    public class DelegateCommand : ICommand
    {
        Action<object> action;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
