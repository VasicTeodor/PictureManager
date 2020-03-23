using PictureManager.Helpers;
using PictureManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.ViewModel
{
    public class AppViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand EditProfile { get; set; }
        public MyICommand AddImage { get; set; }
        public MyICommand MyImages { get; set; }
        public MyICommand LogOut { get; set; }
        private MyImagesViewModel _myImagesViewModel = new MyImagesViewModel();
        private AddImageViewModel _addImageViewModel = new AddImageViewModel();
        private ProfileViewModel _profileViewModel = new ProfileViewModel();
        private BindableBase _currentViewModel;
        private static AppViewModel _instance;

        public static AppViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AppViewModel();

                return _instance;
            }
        }
        public AppViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = _myImagesViewModel;

            EditProfile = new MyICommand(EditMyProfile);
            MyImages = new MyICommand(Images);
            AddImage = new MyICommand(AddNewImage);
            LogOut = new MyICommand(LogOutExecute);
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
                case "images":
                    CurrentViewModel = _myImagesViewModel;
                    break;
                case "addimage":
                    CurrentViewModel = _addImageViewModel;
                    break;
                case "profile":
                    CurrentViewModel = _profileViewModel;
                    break;
            }
        }

        public void EditMyProfile()
        {
            CurrentViewModel = _profileViewModel;
        }

        public void Images()
        {
            CurrentViewModel = _myImagesViewModel;
        }

        public void AddNewImage()
        {
            CurrentViewModel = _addImageViewModel;
        }

        public void LogOutExecute()
        {
            MainViewModel.Instance.OnNav("login");
            MainViewModel.logedInUser = null;
        }
    }
}
