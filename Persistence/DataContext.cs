using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }


        public DbSet<Categories> categories { get; set; }
        public DbSet<Shoes> shoes { get; set; }
        public DbSet<ShoesSize> shoesSizes { get; set; }
        public DbSet<ShoesImages> shoesImages { get; set; }

    }
}
