using ConcertApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.VocalType.Any())
                {
                    context.VocalType.AddRange(
                     new VocalType
                     {
                         Name = "Бас",
                     },

                     new VocalType
                     {
                         Name = "Тенор",
                     },

                    new VocalType
                    {
                        Name = "Альт",
                    }
                    );
                }

                if (!context.EventType.Any())
                {
                    context.EventType.AddRange(
                        new EventType
                        {
                            Name = "Классика",
                        },

                        new EventType
                        {
                            Name = "Вечеринка",
                        },

                        new EventType
                        {
                            Name = "Опен эйр",
                        }
                    );
                }

                if (!context.Location.Any())
                {
                    context.Location.AddRange(
                         new Location
                         {
                             Name = "Club1",
                             Latitude = 53.23f,
                             Longitude = 27.24f
                         },

                         new Location
                         {
                             Name = "Club2",
                             Latitude = 52.56f,
                             Longitude = 27.62f
                         },

                        new Location
                        {
                            Name = "Club3",
                            Latitude = 52.10f,
                            Longitude = 27.13f
                        }
                        );
                }
                context.SaveChanges();

                if (!context.ClassicConcert.Any())
                {
                    context.ClassicConcert.AddRange(
                        new ClassicConcert
                        {
                            Musician = "Kirkorov",
                            Tickets = 1000,
                            Date = DateTime.Parse("2018-3-13"),
                            Location = context.Location.SingleOrDefault(l => l.Name == "Club1"),
                            EventType = context.EventType.SingleOrDefault(e => e.Name == "Классика"),
                            Price = 100500,
                            VocalType = context.VocalType.SingleOrDefault(v => v.Name == "Тенор"),
                            Composer = "Composer1",
                            Name = "Wow Event"
                        },
                        new ClassicConcert
                        {
                            Musician = "Pugacheva",
                            Tickets = 1000,
                            Date = DateTime.Parse("2018-3-14"),
                            Location = context.Location.SingleOrDefault(l => l.Name == "Club2"),
                            EventType = context.EventType.SingleOrDefault(e => e.Name == "Классика"),
                            Price = 100500,
                            VocalType = context.VocalType.SingleOrDefault(v => v.Name == "Тенор"),
                            Composer = "Composer2",
                            Name = "Retro party (still classic)"
                        });
                }

                if (!context.OpenAir.Any())
                {
                    context.OpenAir.AddRange(
                        new OpenAir
                        {
                            Musician = "RockZaBobrov",
                            Tickets = 5000,
                            Date = DateTime.Parse("2018-5-23"),
                            Location = context.Location.SingleOrDefault(l => l.Name == "Club3"),
                            EventType = context.EventType.SingleOrDefault(e => e.Name == "Опен эйр"),
                            Price = 2300,
                            HowToGet = "By car",
                            Headliner = "30 secs to mars"
                        },
                        new OpenAir
                        {
                            Musician = "DJ123",
                            Tickets = 5000,
                            Date = DateTime.Parse("2018-6-11"),
                            Location = context.Location.SingleOrDefault(l => l.Name == "Club2"),
                            EventType = context.EventType.SingleOrDefault(e => e.Name == "Опен эйр"),
                            Price = 2000,
                            HowToGet = "By car",
                            Headliner = "DJ123"
                        });
                }

                if (!context.Party.Any())
                {
                    context.Party.AddRange(
                        new Party
                        {
                            Musician = "Dance1",
                            Tickets = 500,
                            Date = DateTime.Parse("2018-5-23"),
                            Location = context.Location.SingleOrDefault(l => l.Name == "Club3"),
                            EventType = context.EventType.SingleOrDefault(e => e.Name == "Вечеринка"),
                            Price = 1800,
                            MinimumAge = 16
                        },
                        new Party
                        {
                            Musician = "Dance2",
                            Tickets = 2000,
                            Date = DateTime.Parse("2018-6-11"),
                            Location = context.Location.SingleOrDefault(l => l.Name == "Club1"),
                            EventType = context.EventType.SingleOrDefault(e => e.Name == "Вечеринка"),
                            Price = 1500,
                            MinimumAge = 18
                        });
                }
                context.SaveChanges();
            }
        }
    }
}
