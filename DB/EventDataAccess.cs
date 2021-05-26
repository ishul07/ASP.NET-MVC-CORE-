using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class EventDataAccess
    {
        EventDataContext db = new EventDataContext();

        //To create an event
        public void Create(Event @event)
        {
            db.Event.Add(@event);
            db.SaveChanges();
        }

        //Check whether event exists or not
        public bool Exists(Event @event)
        {
            if (db.Event.Any(e => (e.Title == @event.Title && e.StartTime == @event.StartTime && e.Location == @event.Location && e.Date == @event.Date)))
            {
                return true;
            }
            return false;
        }
        //Whether event mmatch during update or not..!
        public bool UpdateExists(Event @event)
        {
            if (db.Event.Any(e => (e.Title == @event.Title && e.StartTime == @event.StartTime &&
            e.Location == @event.Location && e.Date == @event.Date && e.EventID != @event.EventID)))
            {
                return true;
            }
            return false;
        }

        //To update event data
        public void Update(Event @event)
        {
            var @oldevent = db.Event.Find(@event.EventID);

            @oldevent.Title = @event.Title;
            @oldevent.Date = @event.Date;
            @oldevent.Location = @event.Location;
            @oldevent.StartTime = @event.StartTime;
            @oldevent.Type = @event.Type;
            @oldevent.DurationInHours = @event.DurationInHours;
            @oldevent.Description = @event.Description;
            @oldevent.OtherDetails = @event.OtherDetails;
            @oldevent.InviteByEmail = @event.InviteByEmail;
            @oldevent.UserID = @event.UserID;
            db.SaveChanges();

        }

        //To delete an event
        public void Delete(int id)
        {
            var @event = db.Event.Find(id);
            db.Event.Remove(@event);
            db.SaveChanges();
        }

        //To fetch detail of an event
        public Event GetDetails(int id)
        {
            var @event = db.Event.Find(id);
            return @event;
        }

        //To get all event by using userId
        public IEnumerable<Event> SelectAllEvent(int userId)
        {
            var @events = from e in db.Event
                          where (e.UserID == userId)
                          select e;
            return @events;
        }

        //Get all event where user was invited
        public IEnumerable<Event> EventInvitedTo(int userId)
        {
            var email = db.User.Find(userId).Email;
            var @events = db.Event.
                          Where(e => e.InviteByEmail != null && e.InviteByEmail.Contains(email));
            return @events;
        }

        //Is valid Edit
        public bool IsValidEdit(int id, int userId)
        {
            if (db.Event.Find(id).UserID == userId)
            {
                return true;
            }
            return false;
        }
    }
}
