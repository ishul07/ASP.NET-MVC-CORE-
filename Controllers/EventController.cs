using BusinessLogic;
using BusinessObject;
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
    public class EventController : Controller
    {
        private readonly EventService eventService = new EventService();
        private readonly UserService userService = new UserService();
        // GET: Event
        public ActionResult Index()
        {
            var userID = (int)Session["UserID"];
            var @events = eventService.GetEvents(userID);
            return View(@events);
        }


        // GET: Event/Create
        public ActionResult Create()
        {
            List<string> Type = eventService.EventType();
            List<string> StartTime = eventService.StartTimeList();

            ViewBag.Type = Type;
            ViewBag.StartTime = StartTime;
            return View("Create");
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(CreateEvent createEventViewModel)
        {
            var userID = (int)Session["UserID"];
            List<string> Type = eventService.EventType();
            List<string> StartTime = eventService.StartTimeList();
            ViewBag.Type = Type;
            ViewBag.StartTime = StartTime;
            // TODO: Add insert logic here
            if (!ModelState.IsValid)
            {
                return View();
            }
            //Set default type="public"
            if (string.IsNullOrEmpty(Convert.ToString(createEventViewModel.Type)))
            {
                createEventViewModel.Type = "Public";
            }

            EventBO bookEvent = new EventBO()
            {
                Title = createEventViewModel.Title,
                Date = createEventViewModel.Date,
                Location = createEventViewModel.Location,
                StartTime = createEventViewModel.StartTime,
                Type = createEventViewModel.Type,
                DurationInHours = createEventViewModel.DurationInHours,
                Description = createEventViewModel.Description,
                OtherDetails = createEventViewModel.OtherDetails,
                InviteByEmail = createEventViewModel.InviteByEmail,
                UserID = userID,
            };

            if (!eventService.Create(bookEvent))
            {
                ModelState.AddModelError("Title", "Event already exist,please change some fields...!");
                return View();
            }
            else
            {
                ViewBag.Message = "Event created successfully...!";
                return View();
            }

        }


        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            var role = Convert.ToString(Session["Role"]);
            var userID = (int)Session["UserID"];

            List<string> Type = eventService.EventType();
            List<string> StartTime = eventService.StartTimeList();

            ViewBag.Type = Type;
            ViewBag.StartTime = StartTime;

            var @event = eventService.GetDetails(id);
            if (@event == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Is event editable or not
            if (!(eventService.IsValidEdit(id, userID) || userService.IsValidRole(role)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event @event)
        {

            //raw data requred for furtur process or must be initialized
            @event.UserID = id;
            List<string> Type = eventService.EventType();
            List<string> StartTime = eventService.StartTimeList();
            ViewBag.Type = Type;
            ViewBag.StartTime = StartTime;
            var @oldevent = eventService.GetDetails(@event.EventID);


            //Set previous  type
            if (string.IsNullOrEmpty(Convert.ToString(@event.Type)))
            {
                @event.Type = @oldevent.Type;
            }
            //Set previous  type
            if (string.IsNullOrEmpty(Convert.ToString(@event.StartTime)))
            {
                @event.StartTime = oldevent.StartTime;
            }
            // TODO: Add update logic here
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!eventService.Update(@event))
            {

                ModelState.AddModelError("Title", "Unable to save changes. Maybe event already exists...!");
                return View(@event);
            }
            else
            {

                ViewBag.Message = "Updated Successfully..!";
                return View(@event);
            }

        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            var @event = eventService.GetDetails(id);
            if (@event == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event @event)
        {
            try
            {

                eventService.Delete(id);
                return RedirectToAction("AllEvents", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: All Event where user is invited
        public ActionResult EventsInvitedTo()
        {
            var userID = (int)Session["UserID"];
            var @event = eventService.EventInvitedTo(userID);
            return View(@event);
        }
    }
}