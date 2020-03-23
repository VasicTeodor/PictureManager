using PictureManager.Helpers;
using PictureManager.Model;
using PictureManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.View
{
    public class MainViewModel : BindableBase
    {
        public static User logedInUser;
        public MyICommand<string> NavCommand { get; private set; }
        private LogInViewModel _logInViewModel = new LogInViewModel();
        private RegisterViewModel _registerViewModel = new RegisterViewModel();
        private AppViewModel _appViewModel = new AppViewModel();
        private BindableBase _currentViewModel;
        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainViewModel();

                return _instance;
            }
        }
        public MainViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = _logInViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        public void OnNav(string destination)
        {
            switch (destination)
            {
                case "login":
                    CurrentViewModel = _logInViewModel;
                    break;
                case "register":
                    CurrentViewModel = _registerViewModel;
                    break;
                case "app":
                    CurrentViewModel = _appViewModel;
                    break;
            }
        }
    }
}
