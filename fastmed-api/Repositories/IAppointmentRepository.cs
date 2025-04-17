using fastmed_api.Models;

namespace fastmed_api.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment> GetAppointmentByIdAsync(int id);
    Task AddAppointmentAsync(Appointment appointment);
    Task UpdateAppointmentAsync(Appointment appointment);
    Task DeleteAppointmentAsync(int id);
    Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int clinicId);
}