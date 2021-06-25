using HomeDBSqlite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeDBSqlite
{
    public class MyDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Occupant> Occuppant { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Home> Homes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=HomeDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Address>().ToTable("Address", "DB");
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasIndex(e => e.Country);
            });
            

            modelBuilder.Entity<Home>().ToTable("Home", "DB");
            modelBuilder.Entity<Home>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasOne(e => e.Address);
                entity.HasMany(e => e.Owners);
                entity.HasMany(e => e.Rooms);
                entity.HasMany(e => e.Occupants);
            });


            modelBuilder.Entity<Company>().ToTable("Company", "DB");
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasOne(e => e.Headquaters);

            });

            modelBuilder.Entity<Furniture>().ToTable("Furniture", "DB");
            modelBuilder.Entity<Furniture>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasIndex(e => e.Name);
            });

            modelBuilder.Entity<Occupant>().ToTable("Occupant", "DB");
            modelBuilder.Entity<Occupant>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasOne(e => e.PersonOccupant);
            });

            modelBuilder.Entity<Owner>().ToTable("Owner", "DB");
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasOne(e => e.PersonOwner);
                entity.HasOne(e => e.CompanyOwner);
            });

            modelBuilder.Entity<Person>().ToTable("Person", "DB");
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasIndex(e => e.Name);
            });

            modelBuilder.Entity<Room>().ToTable("Room", "DB");
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.OID);
                entity.HasMany(e => e.Doors);
                entity.HasMany(e => e.Windows);
                entity.HasMany(e => e.Furniture);
            });

            modelBuilder.Entity<Window>().ToTable("Window", "DB");
            modelBuilder.Entity<Window>(entity =>
            {
                entity.HasKey(e => e.OID);
            });

            modelBuilder.Entity<Door>().ToTable("Door", "DB");
            modelBuilder.Entity<Door>(entity =>
            {
                entity.HasKey(e => e.OID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
