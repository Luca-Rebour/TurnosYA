using Domain.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<ExternalCustomer> ExternalCustomers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();

            modelBuilder.Entity<Professional>().ToTable("Professionals");
            modelBuilder.Entity<Customer>().ToTable("Customers");

            modelBuilder.Entity<Appointment>().HasKey(a => a.Id);
            modelBuilder.Entity<AvailabilitySlot>().HasKey(a => a.Id);
            modelBuilder.Entity<Notification>().HasKey(a => a.Id);
            modelBuilder.Entity<UserActivity>().HasKey(a => a.Id);
            modelBuilder.Entity<ExternalCustomer>().HasKey(a => a.Id);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Professional)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AvailabilitySlot>()
                .HasOne(slot => slot.Professional)
                .WithMany(p => p.Availability)
                .HasForeignKey(slot => slot.ProfessionalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserActivity>()
            .HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserActivity>()
                .HasOne(a => a.Professional)
                .WithMany()
                .HasForeignKey(a => a.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserActivity>()
                .HasOne(a => a.Appointment)
                .WithMany()
                .HasForeignKey(a => a.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExternalCustomer>()
                .HasOne(a => a.CreatedByProfessional)
                .WithMany()
                .HasForeignKey(a => a.CreatedByProfessionalId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }




    }

}
