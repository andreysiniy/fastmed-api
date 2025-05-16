namespace fastmed_api.DTO;

public class ClinicCardDto
{
    public int ClinicId { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public List<DoctorCardDto> Doctors { get; set; } = new();
    public List<WorkingHourDto> WorkingHours { get; set; } = new();
}