using Adress43_Kurlishuk.Classes.Database;
using Adress43_Kurlishuk.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adress43_Kurlishuk.Context
{
    public class GroupContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public GroupContext()
        {
            Database.EnsureCreated();
            Groups.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(Config.connection);
    }
}
