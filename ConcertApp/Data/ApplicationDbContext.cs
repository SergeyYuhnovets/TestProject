using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConcertApp.Models;

namespace ConcertApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ClassicConcert> ClassicConcert { get; set; }
        public DbSet<Party> Party { get; set; }
        public DbSet<OpenAir> OpenAir { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<VocalType> VocalType { get; set; }
        public DbSet<ConcertEvent> ConcertEvent { get; set; }
    }
}
