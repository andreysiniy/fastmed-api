namespace fastmed_api.DTO;

public class WorkingHourDto
{
    public int Id { get; set; }
    public int DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, ...
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    
    public ClinicCardDto Clinic { get; set; } = null!;
}