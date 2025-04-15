using fastmed_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fastmed_api.Data;

public class FastmedContext : DbContext
{
    public FastmedContext(DbContextOptions<FastmedContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<ClinicCard> ClinicCards { get; set; }
    public DbSet<DoctorCard> DoctorCards { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<WorkingHour> WorkingHours { get; set; }
}