using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fastmed_api.Models;

[Table("Appointment")]
public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("appointment_id")] public int AppointmentId { get; set; }
    [Column("patient_uuid")] public string PatientUuid { get; set; }
    [Column("doctor_id")] public int DoctorId { get; set; }
    [ForeignKey("DoctorId")] public DoctorCard Doctor { get; set; } = null!;
    [Column("patient_name")] public string PatientName { get; set; } = null!;
    [Column("phone")] public string Phone { get; set; } = null!;
    [Column("appointment_time")] public DateTime AppointmentTime { get; set; }
    [Column("notes")] public string? Notes { get; set; }
}