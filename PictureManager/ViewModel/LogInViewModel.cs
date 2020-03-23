using PictureManager.Helpers;
using PictureManager.Model;
using PictureManager.View;
using PictureManager.XMLRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PictureManager.ViewModel
{
    public class LogInViewModel : BindableBase
    {
        private User currentUser = new User();
        private PictureManagerRepository _repo;
        public MyICommand LogInCommand { get; set; }
        public MyICommand RegisterCommand { get; set; }
        public LogInViewModel()
        {
            LogInCommand = new MyICommand(LogIn);
            RegisterCommand = new MyICommand(Register);
            _repo = new PictureManagerRepository();
        }

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if(currentUser != value)
                {
                    currentUser = value;
                    OnPropertyChanged("CurrentUser");
                    LogInCommand.RaiseCanExecuteChanged();
                }
            }
        }
        
        public void Register()
        {
            MainViewModel.Instance.OnNav("register");
        }

        public void LogIn()
        {
            CurrentUser.Validate();

            if (!CurrentUser.IsValid)
            {
                return;
            }
            else if (!_repo.CheckUsername(currentUser.Username))
            {
                MessageBox.Show("User does not exist");
                return;
            }
            else if(!_repo.CheckUsernameAndPassword(currentUser.Username, currentUser.Password))
            {
                MessageBox.Show("Wrong username or password");
                return;
            }
            else
            {
                MainViewModel.logedInUser = _repo.LoadData(CurrentUser.Username);
                MainViewModel.Instance.OnNav("app");
            }
        }
    }
}
