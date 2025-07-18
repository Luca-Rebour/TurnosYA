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
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<InternalAppointment> InternalAppointments { get; set; }
        public DbSet<ExternalAppointment> ExternalAppointments { get; set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<ExternalClient> ExternalClients { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();
            modelBuilder.Entity<Appointment>().UseTptMappingStrategy();

            modelBuilder.Entity<Professional>().ToTable("Professionals");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<InternalAppointment>().ToTable("InternalAppointments");
            modelBuilder.Entity<ExternalAppointment>().ToTable("ExternalAppointments");

            modelBuilder.Entity<AvailabilitySlot>().HasKey(a => a.Id);
            modelBuilder.Entity<Notification>().HasKey(a => a.Id);
            modelBuilder.Entity<UserActivity>().HasKey(a => a.Id);
            modelBuilder.Entity<ExternalClient>().HasKey(a => a.Id);

            modelBuilder.Entity<InternalAppointment>()
                .HasOne(i => i.Professional)
                .WithMany(p => p.InternalAppointments)
                .HasForeignKey(i => i.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExternalAppointment>()
                .HasOne(e => e.Professional)
                .WithMany(p => p.ExternalAppointments)
                .HasForeignKey(e => e.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<InternalAppointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.InternalAppointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ExternalAppointment>()
                .HasOne(a => a.ExternalClient)
                .WithMany(c => c.ExternalAppointments)
                .HasForeignKey(a => a.ExternalClientId)
                .OnDelete(DeleteBehavior.Cascade);








           









            modelBuilder.Entity<AvailabilitySlot>()
                .HasOne(slot => slot.Professional)
                .WithMany(p => p.Availability)
                .HasForeignKey(slot => slot.ProfessionalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserActivity>()
                .HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(a => a.ClientId)
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

            modelBuilder.Entity<ExternalClient>()
                .HasOne(a => a.CreatedByProfessional)
                .WithMany()
                .HasForeignKey(a => a.CreatedByProfessionalId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }




    }

}
