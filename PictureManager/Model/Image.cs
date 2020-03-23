using PictureManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.Model
{
    public class Image : ValidationBase
    {
        private string _imagePath;
        private string _title;
        private string _description;
        private DateTime _date;

        public Image()
        {
            this._date = DateTime.Now;
        }

        public Image(string path, string title, string desc)
        {
            this._imagePath = path;
            this._title = title;
            this._description = desc;
            this._date = DateTime.Now;
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }


        public string Description
        {
            get { return _description; }
            set
            {
                if(_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if(_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if(_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this._title))
            {
                this.ValidationErrors["Title"] = "Title is required.";
                return;
            }
            if (string.IsNullOrWhiteSpace(this._imagePath))
            {
                this.ValidationErrors["ImagePath"] = "Image cannot be empty.";
                return;
            }
        }
    }
}
