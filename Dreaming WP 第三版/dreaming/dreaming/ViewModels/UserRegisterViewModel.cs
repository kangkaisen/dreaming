using DataHelp.Models;
using dreaming.Command;
using dreaming.ControlHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dreaming.ViewModels
{
    class UserRegisterViewModel:ModelBase
    {

        private string phone;

        public string Phone
        {
            get { return phone; }
            set
            { this.SetProperty(ref this.phone, value); }
        }


        private string password;

        public string Password
        {   
            get { return password; }
            set
            { this.SetProperty(ref this.password, value); }
        }


        private string passwordR;
            
        public string PasswordR
        {
            get { return passwordR; }
            set
            { this.SetProperty(ref this.passwordR, value); }
        }

        public DelegateCommand ResgisterCommand { get; set; }

        public UserRegisterViewModel()
        {
            ResgisterCommand = new DelegateCommand(Register);
            HelpMethods.HideStatusBar();
        }

        public void Register()
        {
            if (Phone==null||Phone.Length!= 11)
            {
                HelpMethods.Msg("手机号码不符合规范!");
            }
            else if (Password == null)
            {
                HelpMethods.Msg("密码不能为空!");
            }
            else if (Password.Length<6)
            {
                HelpMethods.Msg("密码的长度要大于6位!");
            }
            else if (Password!=PasswordR)
            {
                HelpMethods.Msg("俩次输入密码不一致!");
            }
            else
            {
                HttpPostRegister.HttpPost(Phone, Password);
            }

        }
        
    }
}
