using AppointmentManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Doctor> Doctors { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
		
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Doctor)  
				.WithMany(d => d.Appointments)  
				.HasForeignKey(a => a.DoctorId); 
		}
	}
}
