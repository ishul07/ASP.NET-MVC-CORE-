using DB;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class EventService
    {
        private readonly EventDataAccess eventDataAccess = new EventDataAccess();
        private EventDataContext db = new EventDataContext();
        //To create an event
        public bool Create(EventBO @event)
        {

            Event eventData = new Event()
            {
                Title = @event.Title,
                Date = @event.Date,
                Location = @event.Location,
                StartTime = @event.StartTime,
                Type = @event.Type,
                DurationInHours = @event.DurationInHours,
                Description = @event.Description,
                OtherDetails = @event.OtherDetails,
                InviteByEmail = @event.InviteByEmail,
                UserID = @event.UserID,
            };
            //check whether event exist or not
            if (eventDataAccess.Exists(eventData))
            {
                return false;
            }
            eventDataAccess.Create(eventData);
            return true;
        }

        //To fetch detail of an event
        public Event GetDetails(int id)
        {
            var @event = eventDataAccess.GetDetails(id);
            return @event;
        }

        //To edit an envent
        public bool Update(Event @event)
        {
            if (eventDataAccess.UpdateExists(@event))
            {
                return false;
            }
            eventDataAccess.Update(@event);
            return true;
        }

        //To delete an event
        public void Delete(int id)
        {
            eventDataAccess.Delete(id);
        }

        //To get all event by using userId
        public IEnumerable<Event> GetEvents(int userId)
        {
            var @events = eventDataAccess.SelectAllEvent(userId);
            return @events;
        }
        public IEnumerable<Event> GetPublicEvents()
        {
            var @events = from e in db.Event
                          where (e.Type == "Public")
                          select e;
            return @events;
        }
        //Get all event where user was invited
        public IEnumerable<Event> EventInvitedTo(int userId)
        {
            var @event = eventDataAccess.EventInvitedTo(userId);
            return @event;
        }

        //To obtain type of event
        public List<string> EventType()
        {
            List<string> Type = new List<string>() { "Public", "Private" };
            return Type;
        }

        //Start Time list used in view
        public List<string> StartTimeList()
        {
            List<string> StartTime = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    StartTime.Add("0" + i + ":00");
                }
                else
                {
                    StartTime.Add(i + ":00");
                }
            }
            return StartTime;

        }
        //IsValid Edit
        public bool IsValidEdit(int id, int userId)
        {
            if (eventDataAccess.IsValidEdit(id, userId))
            {
                return true;
            }
            return false;
        }
    }
}
