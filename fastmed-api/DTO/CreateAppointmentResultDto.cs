namespace fastmed_api.DTO;


public class CreateAppointmentResultDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = null!;
    public DateTime AppointmentTime { get; set; } // Время приема, которое было в запросе
    public string DoctorName { get; set; } = null!; // Имя врача
    public string DoctorSpeciality { get; set; } = null!;  // Специальность врача
    public string ClinicName { get; set; } = null!; // Название клиники

}