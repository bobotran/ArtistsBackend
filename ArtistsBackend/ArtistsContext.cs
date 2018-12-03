using ArtistsBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend
{
    public class ArtistsContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<User> Users { get; set; }

        public ArtistsContext(DbContextOptions<ArtistsContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
