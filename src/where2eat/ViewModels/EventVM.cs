using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace where2eat.ViewModels
{
    public class EventVM
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public bool EventStatus { get; set; }
       
        public IList<OptionVM> Options { get; set; }

    }
}
