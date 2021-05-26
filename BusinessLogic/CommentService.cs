using BusinessObject;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CommentService
    {
        private readonly CommentDataAccess commentDataAccess = new CommentDataAccess();

        public void Create(CommentBO com)
        {
            Comment comment = new Comment()
            {
                CommentID = com.CommentID,
                Comments = com.Comments,
            };
            commentDataAccess.Create(comment);
        }
    }
}
