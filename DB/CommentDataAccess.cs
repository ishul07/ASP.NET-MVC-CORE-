using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class CommentDataAccess
    {
        private readonly EventDataContext db = new EventDataContext();

        //To create a comment
        public void Create(Comment comment)
        {
            db.Comment.Add(comment);
            db.SaveChanges();
        }
    }
}
