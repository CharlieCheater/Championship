using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSniffer.Models
{
    public class MainContext : DbContext
    {
        public MainContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<MedCard> MedCards { get; set; }
        //Конфигурация базы данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=192.168.227.12;Initial Catalog=MedCompany01;User ID=user01;Password=01;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Gender>().HasData(new List<Gender>
            {
                new Gender { Id = 1, Name = "Мужской"},
                new Gender { Id = 2, Name = "Женский"},
            });
            //modelBuilder.Entity<Pacient>(ent =>
            //{
            //    ent.HasOne(x => x.MedCard).WithOne(p => p.Pacient);
            //});
        }
    }
}
