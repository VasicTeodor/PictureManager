using PictureManager.Helpers;
using PictureManager.Model;
using PictureManager.View;
using PictureManager.XMLRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.ViewModel
{
    public class RegisterViewModel : BindableBase
    {
        private User currentUser = new User();
        private string _confirmPassword;
        private string _error;
        private PictureManagerRepository _repo;
        public MyICommand Register { get; set; }
        public MyICommand Cancel { get; set; }

        public RegisterViewModel()
        {
            Register = new MyICommand(AddUser);
            Cancel = new MyICommand(ExecuteCancel);
            _repo = new PictureManagerRepository();
        }

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
                Register.RaiseCanExecuteChanged();
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
                }
            }
        }

        private void ExecuteCancel()
        {
            MainViewModel.Instance.OnNav("login");
        }
        
        private void AddUser()
        {
            Error = "";
            CurrentUser.Validate();
            if (!CurrentUser.IsValid)
            {
                return;
            }
            else if(!CurrentUser.Password.ToLower().Equals(ConfirmPassword.ToLower()))
            {
                Error = "Passwords must match.";
            }
            else if (_repo.CheckUsername(currentUser.Username))
            {
                Error = "Username allready exists.";
            }
            else
            {
                _repo.SaveData(currentUser);
                MainViewModel.logedInUser = CurrentUser;
                MainViewModel.logedInUser.MyImages = new List<Image>();
                MainViewModel.Instance.OnNav("app");
                AppViewModel.Instance.OnNav("addimage");
            }
        }
    }
}
