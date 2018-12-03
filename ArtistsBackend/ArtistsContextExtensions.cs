using ArtistsBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend
{
    public static class ArtistsContextExtensions
    {
        public static void EnsureSeedDataForContext(this ArtistsContext context)
        {
            if(context.Events.Any() && context.Users.Any()) { return; }

            if (!context.Events.Any())
            {
                var mockItems = new List<Event>
                {   
                    new Event {Nickname = "Lakeview", StartTime = new DateTime(2018, 6, 15) },
                    new Event {Nickname = "Laguna Woods Village", StartTime = new DateTime(2018, 6, 17, 19, 0, 0),
                     EndTime = new DateTime(2018, 6, 17, 21, 0, 0), Address = new Address{
                         AddressLine = "24232 Calle Aragon", City = "Laguna Woods", Building = "Clubhouse #1", FloorLevel = 13 },
                     Performances = new List<Performance>{
                         new Performance
                         {
                             Title = "Dance Macabre",
                             Host = "Ryan Tran",
                             Parts = new List<Part>{ new Part { PartName = "Violin"},
                                 new Part{ PartName = "Piano", UserId = 1 } },
                             Composer = "Camille Saint-Saëns",
                             Minutes = 3,
                             Description = "Three days before",
                             //Performers = new List<Performer>{new Performer{Name = "Ryan Tran" },
                             //    new Performer{Name = "Kevin Tran" } }
                         },
                         new Performance
                         {
                             Title = "Cello Suite No.1",
                             Host = "Megan Wei",
                             Parts = new List<Part>{ new Part { PartName = "Cello" } },
                             Composer = "Bach",
                             Minutes = 4,
                             Description = "Played at every frickin senior center",
                             //Performers = new List<Performer>{ new Performer { Name = "Megan Wei" } }
                         }
                     }
                    },
                    new Event {Nickname = "Irvine", StartTime=new DateTime(2018, 7, 5)  },
                    new Event {Nickname = "Tustin Rehabilitation", StartTime=new DateTime(2018, 8, 21)  },
                    new Event {Nickname = "Towers", StartTime=new DateTime(2018, 9, 1)  },
                    new Event {Nickname = "Tiffany's", StartTime=new DateTime(2018, 9, 7)  },
                };
                context.Events.AddRange(mockItems);
            }

            if (!context.Users.Any())
            {
                var user = new User { EmailAddress = "ryanbobotran@gmail.com", Username = "Ryan Tran" };
            }

            context.SaveChanges();
        }
    }
}
