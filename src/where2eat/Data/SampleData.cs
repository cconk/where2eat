using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using where2eat.Models;

namespace where2eat.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure chad (IsAdmin)
            var chad = await userManager.FindByNameAsync("chad.conklin@hotmail.com");
            if (chad == null)
            {
                // create user
                chad = new ApplicationUser
                {
                    UserName = "chad.conklin@hotmail.com",
                    Email = "chad.conklin@hotmail.com",
                    Events = new List<Event>
                    {
                        new Event {
                                    EventName = "Lunch",
                                    EventDate = DateTime.Parse("2017-4-20"),
                                    EventStatus = true,
                                    Options = new List<Option>
                                        {
                                            new Option {OptionName = "Mod", OptionDescription="The best pizza fast!", OptionContributor = "Chad" },
                                            new Option {OptionName = "Republic Pi", OptionDescription="Pizza and other great stuff!", OptionContributor = "Mary" },
                                            new Option {OptionName = "Method", OptionDescription="Healthy and refreshing", OptionContributor = "Sherrie" },
                                            new Option {OptionName = "The Elk", OptionDescription= "Yum", OptionContributor = "Cliff" }
                                        }
                                  },
                        new Event {
                                    EventName ="Dinner",
                                    EventDate =DateTime.Parse("2017-4-20"),
                                    EventStatus = false,
                                    Options = new List<Option>
                                        {
                                            new Option {OptionName = "Mod", OptionDescription="The best pizza fast!", OptionContributor = "Chad" },
                                            new Option {OptionName = "Perry Street", OptionDescription="Pizza and other great stuff!", OptionContributor = "Mary" },
                                            new Option {OptionName = "Manito Tap House", OptionDescription="Beer and burgers", OptionContributor = "Sherrie" },
                                            new Option {OptionName = "Two Seven", OptionDescription= "Delish", OptionContributor = "Cliff" }
                                        }
                                  }
                    }
                };
                await userManager.CreateAsync(chad, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(chad, new Claim("IsAdmin", "true"));
            }

        }

    }
}
