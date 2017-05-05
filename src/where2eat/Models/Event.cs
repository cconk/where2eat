using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace where2eat.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public bool EventStatus { get; set; }
        public ApplicationUser Organizer { get; set; }
        [ForeignKey("Organizer")]
        public string OrganizerId { get; set; }
        public IList<Option> Options { get; set; }

    }
}
