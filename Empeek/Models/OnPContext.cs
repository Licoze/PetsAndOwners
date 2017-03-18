using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SQLite.CodeFirst;
namespace Empeek.Models
{
    public class OnPContext:DbContext
    {
       public OnPContext():base("OwnersAndPets")
        {
           
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<OnPContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}