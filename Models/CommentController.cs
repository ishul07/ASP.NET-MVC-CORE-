using BusinessLogic;
using BusinessObject;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Models
{
    public class CommentController : Controller
    {
        private readonly CommentService commentService = new CommentService();

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        [OverrideAuthentication]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        [OverrideAuthentication]
        public ActionResult Create(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            CommentBO commentData = new CommentBO()
            {
                Comments = comment.Comments,
                DateTime = DateTime.Now,
                EventID = id,
            };
            commentService.Create(commentData);

            return RedirectToAction("Details", "Home", new { id });
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comment/Edit/5
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

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
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