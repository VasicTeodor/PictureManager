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
    public class ProfileViewModel : BindableBase
    {
        private PictureManagerRepository _repo;
        private User newUser = new User();
        private string _error;
        private string _oldPassword;
        public MyICommand Change { get; set; }

        public ProfileViewModel()
        {
            Change = new MyICommand(ChangeProfile);
            _repo = new PictureManagerRepository();
            if (MainViewModel.logedInUser != null)
            {
                NewUser.Username = MainViewModel.logedInUser.Username;
            }
        }
        
        public User NewUser
        {
            get { return newUser; }
            set
            {
                if(newUser != value)
                {
                    newUser = value;
                    OnPropertyChanged("CurrentUser");
                    Change.RaiseCanExecuteChanged();
                }
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return _oldPassword;
            }
            set
            {
                if (_oldPassword != value)
                {
                    _oldPassword = value;
                }
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

        private void ChangeProfile()
        {
            Error = "";
            NewUser.Validate();
            if (!NewUser.IsValid)
            {
                return;
            }
            else if (!string.IsNullOrEmpty(ConfirmPassword) && !MainViewModel.logedInUser.Password.ToLower().Equals(ConfirmPassword.ToLower()))
            {
                Error = "Passwords must match.";
            }
            else if (!NewUser.Username.ToLower().Equals(MainViewModel.logedInUser.Username.ToLower()))
            {
                if(_repo.CheckUsername(NewUser.Username))
                    Error = "Username allready exists.";
            }
            else
            {
                _repo.EditUser(MainViewModel.logedInUser, NewUser);
                MainViewModel.logedInUser = _repo.LoadData(NewUser.Username);
                AppViewModel.Instance.OnNav("images");
                AppViewModel.Instance.Images();
                NewUser.Username = "";
                NewUser.Password = "";
                ConfirmPassword = "";
            }
        }
    }
}
