using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HungryDevs.Models;

namespace HungryDevs.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User usr)
        {
            User u = UsersManager.Current.VerifyUserByPassword(usr.UserName, usr.Password);
            if (u != null)
            {
                UsersManager.Current.currentUser = u;
                Session["currentUser"] = u;
                return RedirectToAction("Index");
            }

            ViewBag.LoginValidation = "Log in attempt not successfull, please try again.";
            return View();
        }
        // GET: Users
        public ActionResult Index()
        {
            //User u = UsersManager.Current.currentUser;
            User u = Session["currentUser"] as User;
            if (u!=null)
            {
                //TempData["currentUser"] = string.Format("{0}-{1}", u.FirstName, u.Id);
                ViewBag.HelloUser = string.Format("{0}-{1}", u.FirstName, u.Id);
                List<User> colletion = UsersManager.Current.GetAllUsers();
                return View(colletion);
            }
            return RedirectToAction("Login", "Users");
        }

        public ActionResult Create()
        {
            //User u = UsersManager.Current.currentUser;
            User u = Session["currentUser"] as User;
            if (u!=null)
            {
                ViewBag.HelloUser = string.Format("{0}-{1}", u.FirstName, u.Id);
                return View();
            }
            return RedirectToAction("Login", "Users");
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(User usr)
        {
            try
            {
                UsersManager.Current.SaveUser(usr);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Register(User usr)
        {
            try
            {
                usr.IsAdmin = false;
                UsersManager.Current.SaveUser(usr);
                Session["currentUser"] = usr;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            //User u = UsersManager.Current.currentUser;
            User u = Session["currentUser"] as User;
            if (u!=null)
            {
                ViewBag.HelloUser = string.Format("{0}-{1}", u.FirstName, u.Id);
                return View(UsersManager.Current.GetUser(id));
            }
            return RedirectToAction("Login", "Users");

        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User usr)
        {
            try
            {
                if (string.IsNullOrEmpty(usr.Password) || usr.Password.Equals(Request["Confirm Password"]))
                {
                    User u = Session["currentUser"] as User; 
                    UsersManager.Current.SaveUser(usr);
                    ViewBag.HelloUser = string.Format("{0}-{1}", usr.FirstName, usr.Id);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            //User u = UsersManager.Current.currentUser;
            User u = Session["currentUser"] as User; 
            if (u!=null)
            {
                ViewBag.HelloUser = string.Format("{0}-{1}", u.FirstName, u.Id);
                return View(UsersManager.Current.GetUser(id));
            }
            return RedirectToAction("Login", "Users");
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, User usr)
        {
            try
            {
                //User u = UsersManager.Current.currentUser;
                User u = Session["currentUser"] as User;
                ViewBag.HelloUser = string.Format("{0}-{1}", u.FirstName, u.Id);
                UsersManager.Current.RemoveUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logoff()
        {
            //UsersManager.Current.currentUser = null;
            Session["currentUser"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
