using Microsoft.Win32;
using PictureManager.Helpers;
using PictureManager.Model;
using PictureManager.View;
using PictureManager.XMLRepository;
using System;

namespace PictureManager.ViewModel
{
    public class AddImageViewModel : BindableBase
    {
        private PictureManagerRepository _repo;
        private Image newImage = new Image();
        private string _info;
        public MyICommand AddImage { get; set; }
        public MyICommand ChoseImage { get; set; }
        public AddImageViewModel()
        {
            AddImage = new MyICommand(AddImageExecute);
            ChoseImage = new MyICommand(OpenImage);
            _repo = new PictureManagerRepository();
        }

        public Image NewImage
        {
            get
            {
                return newImage;
            }
            set
            {
                if(newImage != value)
                {
                    newImage = value;
                    OnPropertyChanged("NewImage");
                    Info = "";
                }
            }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                if(_info != value)
                {
                    _info = value;
                    OnPropertyChanged("Info");
                }
            }
        }

        public void OpenImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                NewImage.ImagePath = (new Uri(op.FileName)).ToString();
            }
        }

        public void AddImageExecute()
        {
            NewImage.Validate();
            if (!NewImage.IsValid)
            {
                return;
            }
            else
            {
                NewImage.Date = DateTime.Now;
                _repo.AddImage(MainViewModel.logedInUser, NewImage);
                MainViewModel.logedInUser.MyImages.Add(NewImage);
                AppViewModel.Instance.Images();
                AppViewModel.Instance.OnNav("images");
                Info = "Picture added to My Pictures!";
            }
        }
    }
}
