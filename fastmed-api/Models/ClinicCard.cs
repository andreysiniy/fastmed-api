using System.ComponentModel.DataAnnotations.Schema;

namespace fastmed_api.Models;

[Table("ClinicCard")]
public class ClinicCard
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("clinic_id")] public int ClinicId { get; set; }
    [Column("name")] public string Name { get; set; } = null!;
    [Column("category")] public string Category { get; set; } = null!;
    [Column("location")] public string Location { get; set; } = null!;
    [Column("phone")] public string Phone { get; set; } = null!;
    
    public ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
    public ICollection<DoctorCard> Doctors { get; set; } = new List<DoctorCard>();
}

[Table("WorkingHours")]
public class WorkingHour
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")] public int Id { get; set; }
    [Column("clinic_id")] public int ClinicId { get; set; }
    [ForeignKey("ClinicId")] public ClinicCard Clinic { get; set; } = null!;
    [Column("day_of_week")] public int DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, ...
    [Column("open_time")] public TimeSpan OpenTime { get; set; }
    [Column("close_time")] public TimeSpan CloseTime { get; set; }
}
