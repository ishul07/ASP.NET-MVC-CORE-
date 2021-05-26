using BusinessLogic;
using BusinessObject;
using DB;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService userService = new UserService();


        // GET: Login/login
        public ActionResult Login()
        {
            return View("Login");
        }

        // POST: Login/login
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = new User()
            {
                Email = login.Email,
                Password = login.Password,
            };
            //Is valid user or not
            if (userService.IsValid(user))
            {
                Session["IsLoggedIn"] = true;

                //fetch userID and role of a user
                var userID = userService.GetDetails(user.Email).First().UserID;
                var role = userService.GetRole(user.Email);

                Session["Role"] = role;
                Session["UserID"] = userID;

                if (role.Equals("Admin"))
                    return RedirectToAction("AllEvents", "Home");

                return RedirectToAction("IndexLogin", "Home");
            }
            else
            {

                ModelState.AddModelError("Password", "Invalid Email address or Password....!!");
                return View();
            }

        }
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

    }
}