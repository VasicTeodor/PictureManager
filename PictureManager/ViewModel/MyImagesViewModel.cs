using PictureManager.Model;
using PictureManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.ViewModel
{
    public class MyImagesViewModel : BindableBase
    {
        public ObservableCollection<Image> Images { get; set; }

        public MyImagesViewModel()
        {
            if (MainViewModel.logedInUser != null)
            {
                if(MainViewModel.logedInUser.MyImages != null)
                    Images = new ObservableCollection<Image>(MainViewModel.logedInUser.MyImages);
            }
        }
    }
}
