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
    public class OptionsService
    {
        public IGenericRepository _repo;

        public OptionsService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<OptionVM> ListAllOptions()
        {
            var options = (from o in _repo.Query<Option>()
                          select new OptionVM()
                          {
                              OptionName = o.OptionName,
                              OptionDescription = o.OptionDescription,
                              OptionContributor = o.OptionContributor,
                              OptionValue = o.OptionValue
                          }).ToList();
            return options;
        }

        public IList<OptionVM> ListAllOptionsByEvent(int Id)
        {
            var selectedEvent = (from e in _repo.Query<Event>()
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

        public void AddNewOption(int id, Option newOption)
        {
            var selectedEvent = (from e in _repo.Query<Event>().Include(e => e.Options)
                                     where e.Id == id
                                     select e).FirstOrDefault();
            var optionToAdd = new Option()
            {
                OptionName = newOption.OptionName,
                OptionDescription = newOption.OptionDescription,
                OptionContributor = newOption.OptionContributor,
                OptionValue = newOption.OptionValue
            };

            selectedEvent.Options.Add(optionToAdd);
            _repo.Update(selectedEvent);
            _repo.SaveChanges();
        }

        public void UpdateOption (Option optionToUpdate)
        {
            var optionUpdate = (from o in _repo.Query<Option>()
                                where o.Id == optionToUpdate.Id
                                select new Option()
                                {
                                    Id = o.Id,
                                    OptionName = o.OptionName,
                                    OptionDescription = o.OptionDescription,
                                    OptionContributor = o.OptionContributor,
                                    OptionValue = o.OptionValue
                                }).FirstOrDefault();
            optionUpdate.OptionName = optionToUpdate.OptionName;
            optionUpdate.OptionDescription = optionToUpdate.OptionDescription;
            optionUpdate.OptionContributor = optionToUpdate.OptionContributor;
            optionUpdate.OptionValue = optionToUpdate.OptionValue;
            _repo.Update(optionUpdate);
            _repo.SaveChanges();
        }

        public void DeleteOption(int id)
        {
            var optionToDelete = (from o in _repo.Query<Option>()
                                 where o.Id == id
                                 select o).FirstOrDefault();
            _repo.Delete(optionToDelete);
        }
    }
}
