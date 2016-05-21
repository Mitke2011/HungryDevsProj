using HungryDevs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HungryDevs
{
    public class UsersManager
    {
        List<User> collection;
        static UsersManager instance;
        public static UsersManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new UsersManager();
                }
                return instance;
            }
        }
        private UsersManager()
        {
            collection = new List<Models.User>();
        }
        
        public List<Models.User> InitializeUsers()
        {
            this.collection.Add(new User { Id = 1, UserName = "smitic", Password = "Demonoid123", FirstName = "Stefan", LastName = "Mitic", IsAdmin = true, Email = "smitic@calliduscloud.com" });
            this.collection.Add(new User { Id = 2, UserName = "pedjoni", Password = "Tutuban", FirstName = "Predrag", LastName = "Katancevic", IsAdmin = true, Email = "pkatance@calliduscloud.com" });
            return this.collection;
        }

        private List<Models.User> AddNewUser(User newUsr)
        {
            int id = collection.Max(usr => usr.Id) + 1;
            newUsr.Id = id;
            this.collection.Add(newUsr);
            return collection;
        }

        public List<Models.User> RemoveUser(int usrId)
        {
            this.collection.Remove(this.collection.Find(usr => usr.Id == usrId));
            return collection;
        }

        public Models.User GetUser(int id)
        {
            return collection.FirstOrDefault(usr => usr.Id == id);
        }

        public User FindUserByUsername(string username)
        {
            if (this.collection.Count == 0)
            {
                InitializeUsers();
            }

            User result = this.collection.Find(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            return result;
        }

        public User VerifyUserByPassword(string username, string password)
        {
            User u = FindUserByUsername(username);
            if (u!=null && string.Equals(u.Password, password))
            {
                return u;
            }
            return null;
        }

        public List<Models.User> GetAllUsers()
        {
            if (this.collection.Count == 0)
            {
                InitializeUsers();
            }
            return this.collection;
        }

        public void SaveUser(User usr)
        {
            if (usr.Id != 0)
            {
                EditUser(usr);
            }
            else
            {
                AddNewUser(usr);
            }
        }

        private void EditUser(User usr)
        {
            User temp = collection.Find(user => user.Id == usr.Id);
            temp.FirstName = usr.FirstName;
            temp.LastName = usr.LastName;
            temp.Email = usr.Email;
            temp.IsAdmin = usr.IsAdmin;
            temp.UserName = usr.UserName;

            if (!string.IsNullOrEmpty(usr.Password))
            {
                temp.Password = usr.Password;
            }
        }
    }
}