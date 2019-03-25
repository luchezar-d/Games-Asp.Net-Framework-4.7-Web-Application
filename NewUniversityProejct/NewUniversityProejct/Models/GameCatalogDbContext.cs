using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NewUniversityProejct.Models;

namespace UniversityProject.Models
{
    public class GameCatalogDbContext : DbContext
    {
        public DbSet<Game> Game { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}