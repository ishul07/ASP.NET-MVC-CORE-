using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class CreateEvent
    {
        [Required]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        public string Type { get; set; }

        [Display(Name = "Duration in hours"), Range(0, 4)]
        public int DurationInHours { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string Description { get; set; }

        [Display(Name = "Other Details")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed")]
        public string OtherDetails { get; set; }

        [Display(Name = "Invite by email")]
        public string InviteByEmail { get; set; }
    }
}