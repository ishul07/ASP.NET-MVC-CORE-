using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Comment
    {
        public int CommentID { get; set; }

        [Required]
        public string Comments { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int EventID { get; set; }

        public virtual Event Event { get; set; }
    }
}
