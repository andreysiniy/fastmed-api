using fastmed_api.Models;

namespace fastmed_api.Repositories;

public interface IWorkingHourRepository
{
    Task<IEnumerable<WorkingHour>> GetAllWorkingHoursAsync();
    Task<WorkingHour> GetWorkingHourByIdAsync(int id);
    Task AddWorkingHourAsync(WorkingHour workingHour);
    Task UpdateWorkingHourAsync(WorkingHour workingHour);
    Task DeleteWorkingHourAsync(int id);
    Task<IEnumerable<WorkingHour>> GetWorkingHoursByClinicIdAsync(int clinicId);
}