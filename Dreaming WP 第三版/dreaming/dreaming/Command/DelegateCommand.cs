using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace dreaming.Command
{
    public class DelegateCommand : ICommand
    {
        private Action<Object> ExecuteParamAction { get; set; }
        private Action ExecuteAction { get; set; }

        private bool canExecute = true;
        public bool CanExecutes
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    RaiseExecuteChanged();
                }
            }
        }
        public DelegateCommand(Action execute, bool canExecute = true)
        {
            this.ExecuteAction = execute;
            this.canExecute = canExecute;
        }
        public DelegateCommand(Action<object> executeParam, bool canExecute = true)
        {
            this.ExecuteParamAction = executeParam;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            if (this.ExecuteAction != null)
            {
                this.ExecuteAction();
            }
            else if (this.ExecuteParamAction != null)
            {
                this.ExecuteParamAction(parameter);
            }
            
        }



    }
}
