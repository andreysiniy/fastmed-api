using System.ComponentModel.DataAnnotations.Schema;

namespace fastmed_api.Models;

[Table("DoctorCard")]
public class DoctorCard
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("doctor_id")] public int DoctorId { get; set; }
    [Column("name")] public string Name { get; set; } = null!;
    [Column("experience")] public int Experience { get; set; }
    [Column("phone")] public string PhoneNumber { get; set; } = null!;
    [Column("notes")] public string? Notes { get; set; } 
    [Column("clinic_id")] public int ClinicId { get; set; }
    [ForeignKey("ClinicId")] public ClinicCard Clinic { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

}