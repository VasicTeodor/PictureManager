using PictureManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PictureManager.XMLRepository
{
    class UserDB
    {
        private readonly string fileName = "Users.xml";
        public UserDB() { }

        public bool CheckIfUserExists(string username)
        {
            if (File.Exists(fileName))
            {
                XDocument xmlDocument = XDocument.Load(fileName);

                bool retVal = (from user in xmlDocument.Root.Elements("User")
                               where user.Element("Username").Value.ToString().ToLower().Equals(username.ToLower())
                               select user).Any();

                return retVal;
            }
            else
            {
                return false;
            }
        }

        public void EditUserProfile(User user, User newUser)
        {
            if (File.Exists(fileName))
            {
                XDocument xmlDocument = XDocument.Load(fileName);

                xmlDocument.Element("Users")
                                        .Elements("User")
                                        .Where(x => x.Attribute("Id").Value == user.Username.ToString()).FirstOrDefault()
                                        .SetElementValue("Password", newUser.Password);
                xmlDocument.Element("Users")
                                        .Elements("User")
                                        .Where(x => x.Attribute("Id").Value == user.Username.ToString()).FirstOrDefault()
                                        .SetElementValue("Username", newUser.Username);
                xmlDocument.Element("Users")
                                        .Elements("User")
                                        .Where(x => x.Attribute("Id").Value == user.Username.ToString()).FirstOrDefault()
                                        .SetAttributeValue("Id", newUser.Username);

                xmlDocument.Save(fileName);
            }
        }
        public bool LogIn(string username, string password)
        {
            if (File.Exists(fileName))
            {
                XDocument xmlDocument = XDocument.Load(fileName);

                bool retVal = (from user in xmlDocument.Root.Elements("User")
                               where (user.Element("Username").Value.ToString().ToLower().Equals(username.ToLower()) && user.Element("Password").Value.ToString() == password)
                               select user).Any();

                return retVal;
            }
            else
            {
                return false;
            }
        }

        public void NewUser(User user)
        {
            if (!File.Exists(fileName))
            {
                XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),

                new XElement("Users",
                new XElement("User", new XAttribute("Id", user.Username),
                new XElement("Username", user.Username),
                new XElement("Password", user.Password))
                ));

                xmlDocument.Save(fileName);
            }
            else
            {
                try
                {
                    FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    XDocument doc = XDocument.Load(stream);
                    XElement users = doc.Element("Users");
                    users.Add(new XElement("User", new XAttribute("Id", user.Username),
                                  new XElement("Username", user.Username),
                                  new XElement("Password", user.Password)));
                    doc.Save(fileName);
                }
                catch { }
            }
        }

        public User RetriveUserByUserName(string Name)
        {
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XDocument doc = XDocument.Load(stream);
                IEnumerable<User> users =
                    doc.Root
                    .Elements("User")
                    .Where(x => x.Element("Username").Value.ToLower() == Name.ToLower())
                    .Select(userx => new User
                    {
                        Username = userx.Element("Username").Value,
                        Password = userx.Element("Password").Value
                    }).ToList();

                User user = users.First(x => x.Username.ToLower().Equals(Name.ToLower()));

                return user;
            }
            else
            {
                return null;
            }
        }

        public void AddUserImage(User currentUser, Image image)
        {
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XDocument xmlDocument = XDocument.Load(stream);

                IEnumerable<XElement> users = xmlDocument.Root.Elements("User").Where(x => x.Element("Username").Value.ToLower().Equals(currentUser.Username.ToLower()));
                XElement user = users.First();

                user.Add(new XElement("Image",
                              new XElement("Title", image.Title),
                              new XElement("Description", image.Description),
                              new XElement("Date", image.Date.ToString()),
                              new XElement("Path", image.ImagePath)));

                xmlDocument.Save(fileName);
            }
        }

        public IEnumerable<Image> GetUserImages(string currentUser)
        {
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XDocument xmlDocument = XDocument.Load(stream);

                IEnumerable<XElement> users = xmlDocument.Root.Elements("User").Where(x => x.Element("Username").Value.ToLower().Equals(currentUser.ToLower()));
                XElement user = users.First();

                IEnumerable<Image> images = user.Elements("Image").Select(img => new Image
                {
                    Title = img.Element("Title").Value,
                    Description = img.Element("Description").Value,
                    Date = DateTime.Parse(img.Element("Date").Value),
                    ImagePath = img.Element("Path").Value

                }).ToList();


                return images;
            }
            else
            {
                return null;
            }
        }
    }
}
