using BusinessLogic;
using DB;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    
    public class HomeController : Controller
    {

        private EventDataContext db = new EventDataContext();
        private readonly UserService userService = new UserService();
        [OverrideAuthentication]
        public ActionResult IndexLogin()
        {
            var @events = from e in db.Event
                          where (e.Type == "Public")
                          select e;
            return View(@events);
        }
        [OverrideAuthentication]
        public ActionResult Index()
        {
            var @events = from e in db.Event
                          where (e.Type == "Public")
                          select e;
            return View(@events);
        }
        public ActionResult CustomerSupport()
        {
            return Redirect("http://helpdesk.nagarro.com");
        }
        public ActionResult AllEvents()
        {
            var @events = from e in db.Event
                          select e;

            return View(@events);
        }

        [OverrideAuthentication]
        public ActionResult About()
        {
            ViewBag.Message = "Book Reading Event Application";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [OverrideAuthentication]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);

            if (@event == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrEmpty(Convert.ToString(Session["IsLoggedIn"])))
            {
                if (@event.Type.ToString().Equals("Private"))
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            else
            {
                if (!Convert.ToString(Session["Role"]).Equals("Admin") && @event.Type.ToString().Equals("Private") && !@event.UserID.ToString().Equals(Session["UserID"].ToString()))
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            var countOfEmail = 0;
            var invitedByEmail = @event.InviteByEmail;
            if (invitedByEmail != null)
                countOfEmail = invitedByEmail.Split(',').Length;


            DisplayEvent displayEventViewModel = new DisplayEvent()
            {
                EventID = @event.EventID,
                Title = @event.Title,
                Date = @event.Date,
                Description = @event.Description,
                DurationInHours = @event.DurationInHours,
                Location = @event.Location,
                OtherDetails = @event.OtherDetails,
                StartTime = @event.StartTime,
                TotalInvitedToEvent = countOfEmail,
                Comments = @event.Comments,
            };
            return View("Details", displayEventViewModel);
        }
    }
}