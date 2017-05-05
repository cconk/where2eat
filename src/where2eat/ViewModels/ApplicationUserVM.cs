using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using where2eat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace where2eat.ViewModels
{
    public class ApplicationUserVM : IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<EventVM> Events { get; set; }

    }
}
