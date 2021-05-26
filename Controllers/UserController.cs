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
    public class UserController : Controller
    {
        private readonly UserService userService = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        [OverrideAuthentication]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [OverrideAuthentication]
        public ActionResult Create(CreateUser createUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // TODO: Add insert logic here
            UserBO user = new UserBO()
            {
                Email = createUserViewModel.Email,
                Password = createUserViewModel.Password,
                FullName = createUserViewModel.FullName,
            };

            if (!userService.Create(user))
            {
                ModelState.AddModelError("Email", "User already exist,please enter other email...!");
                return View();
            }
            else
            {
                Session["Message"] = "Registration Successfull...!";
                return RedirectToAction("Index","Home");
            }

        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}