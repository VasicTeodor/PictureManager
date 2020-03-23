using PictureManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PictureManager.Model
{
    public class User : ValidationBase
    {
        private string _username;
        private string _password;
        private List<Image> _myImages;

        public List<Image> MyImages
        {
            get { return _myImages; }
            set { _myImages = value; }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if(_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if(_username != value)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }


        protected override void ValidateSelf()
        {
            
            if (string.IsNullOrWhiteSpace(this._username))
            {
                this.ValidationErrors["Username"] = "Username is required.";
                return;
            }
            if (char.IsNumber(this._username[0]))
            {
                this.ValidationErrors["Username"] = "Username can't start with number.";
                return;
            }
            if (string.IsNullOrWhiteSpace(this._password))
            {
                this.ValidationErrors["Password"] = "Password cannot be empty.";
                return;
            }
            if(this._password.Length <= 6)
            {
                this.ValidationErrors["Password"] = "Password must be at least 7 characters.";
                return;
            }
        }
    }
}
