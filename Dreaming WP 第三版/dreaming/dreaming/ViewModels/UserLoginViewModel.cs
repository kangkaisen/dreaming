using DataHelp.Models;
using dreaming.Command;
using dreaming.ControlHelp;
using dreaming.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dreaming.ViewModels
{
    class UserLoginViewModel:ModelBase
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

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ResgisterCommand { get; set; }

        public UserLoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
            ResgisterCommand = new DelegateCommand(Register);
            HelpMethods.HideStatusBar();
        }


        public void Login()

        {
            if (phone == null || Password == null)
            {
                HelpMethods.Msg("请您输入完整手机号和密码!");
            }
            else
            {
                HttpPostLogin.HttpPost(Phone, Password);
            }
            
        }

        public void Register()
        {
            NavigationHelp.NavigateTo(typeof(UserRegister));
        }
    }
}
