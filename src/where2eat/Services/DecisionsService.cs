using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using where2eat.Models;
using where2eat.ViewModels;
using where2eat.Infrastructure;

namespace where2eat.Services
{
    public class DecisionsService
    {
        public IGenericRepository _repo;

        public DecisionsService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<OptionVM> ListWinningOptionsByEvent(int Id)
        {
            var selectedEvent = (from e in _repo.Query<Event>().Include(e=>e.Options.Max(o=>o.OptionValue))
                                 where e.Id == Id
                                 select new EventVM()
                                 {
                                     Options = (from o in e.Options
                                                select new OptionVM()
                                                {
                                                    Id = o.Id,
                                                    OptionName = o.OptionName,
                                                    OptionDescription = o.OptionDescription,
                                                    OptionContributor = o.OptionContributor,
                                                    OptionValue = o.OptionValue
                                                }).ToList()
                                 }).FirstOrDefault();
            return selectedEvent.Options;
        }
    }
}
