namespace fastmed_api.DTO;

public class CreateAppointmentDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = null!;
    public string PatientUuid { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime AppointmentTime { get; set; }
    public string? Notes { get; set; }
    public int? DoctorId { get; set; }
    public string? DoctorName { get; set; } 
}