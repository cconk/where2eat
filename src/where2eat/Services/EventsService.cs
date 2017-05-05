using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using where2eat.Infrastructure;
using where2eat.ViewModels;
using where2eat.Models;

namespace where2eat.Services
{
    public class EventsService
    {
        public IGenericRepository _repo;

        public EventsService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<EventVM> ListAllEvents()
        {
            var events = (from e in _repo.Query<Event>()
                          select new EventVM()
                          {
                              EventName = e.EventName,
                              EventDate = e.EventDate
                          }).ToList();
            return events;
        }

        public IList<EventVM> ListAllEventsByOrganizer(string id)
        {
            var selectedOrganizer = (from i in _repo.Query<ApplicationUser>()
                                     where i.UserName == id
                                     select new ApplicationUserVM()
                                     {
                                         Id = i.Id,
                                         UserName = i.UserName,
                                         Events = (from e in i.Events
                                                   select new EventVM()
                                                   {
                                                       Id = e.Id,
                                                       EventName = e.EventName,
                                                       EventDate = e.EventDate
                                                   }).ToList()
                                     }).FirstOrDefault();
            return selectedOrganizer.Events;
        }

        public void AddNewEvent (string id, Event newEvent)
        {
            var selectedOrganizer = (from i in _repo.Query<ApplicationUser>().Include(i => i.Events)
                                     where i.UserName == id
                                     select i).FirstOrDefault();
            var eventToAdd = new Event()
            {
                EventName = newEvent.EventName,
                EventDate = newEvent.EventDate,
                EventStatus = newEvent.EventStatus
            };

            selectedOrganizer.Events.Add(eventToAdd);
            _repo.Update(selectedOrganizer);
        }

        public void DeleteEvent (int id)
        {
            var eventToDelete = (from e in _repo.Query<Event>()
                                 where e.Id == id
                                 select e).FirstOrDefault();
            _repo.Delete(eventToDelete);
        }
    }
}
