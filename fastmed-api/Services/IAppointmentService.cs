using fastmed_api.DTO;

namespace fastmed_api.Services;

public interface IAppointmentService
{
    Task<List<AppointmentDto>> GetAppointmentsByUuid(string uuid);
    Task<AppointmentDto> GetAppointmentById(int id);
    Task<List<AppointmentDto>> GetAppointments();
    Task<AppointmentDto> CreateAppointment(AppointmentDto appointment);
    Task DeleteAppointment(int id);
    Task UpdateAppointment(int id, AppointmentDto appointment);
}