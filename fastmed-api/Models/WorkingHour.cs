using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fastmed_api.Models;

[Table("WorkingHours")]
public class WorkingHour
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")] public int Id { get; set; }
    [Column("clinic_id")] public int ClinicId { get; set; }
    [ForeignKey("ClinicId")] public ClinicCard Clinic { get; set; } = null!;
    [Column("day_of_week")] public int DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, ...
    [Column("open_time")] public TimeSpan OpenTime { get; set; }
    [Column("close_time")] public TimeSpan CloseTime { get; set; }
}