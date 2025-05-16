namespace fastmed_api.DTO;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime AppointmentTime { get; set; }
    public string? Notes { get; set; }
}