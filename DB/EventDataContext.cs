using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class EventDataContext : DbContext
    {
        
        public DbSet<Event> Event { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}
