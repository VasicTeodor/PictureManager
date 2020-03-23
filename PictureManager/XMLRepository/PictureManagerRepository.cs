using PictureManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.XMLRepository
{
    public class PictureManagerRepository
    {
        private readonly UserDB _userDb = new UserDB();

        public PictureManagerRepository() { }

        public bool SaveData(User user)
        {
            _userDb.NewUser(user);
            return true;
        }

        public void AddImage(User user, Image image)
        {
            _userDb.AddUserImage(user, image);
        }

        public User LoadData(string username)
        {
            User retVal = _userDb.RetriveUserByUserName(username);
            retVal.MyImages = _userDb.GetUserImages(username).ToList();
            return retVal;
        }

        public void EditUser(User oldUser, User newUser)
        {
            _userDb.EditUserProfile(oldUser, newUser);
        }

        public bool CheckUsername(string username)
        {
            return _userDb.CheckIfUserExists(username);
        }

        public bool CheckUsernameAndPassword(string username, string password)
        {
            return _userDb.LogIn(username, password);
        }
    }
}
