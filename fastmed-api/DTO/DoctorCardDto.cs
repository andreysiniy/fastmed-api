namespace fastmed_api.DTO;

public class DoctorCardDto
{
    public int DoctorId { get; set; }
    public string Name { get; set; } = null!;
    public int Experience { get; set; }
    public string Speciality { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Notes { get; set; }
    public int ClinicId { get; set; }


    public List<AppointmentDto> Appointments { get; set; } = new();
}