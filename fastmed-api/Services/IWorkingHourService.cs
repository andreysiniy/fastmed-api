using fastmed_api.DTO;

namespace fastmed_api.Services;

public interface IWorkingHourService
{
    Task<List<WorkingHourDto>> GetWorkingHours();
    Task<WorkingHourDto> GetWorkingHour(int id);
    Task<WorkingHourDto> CreateWorkingHour(WorkingHourDto workingHour);
    Task DeleteWorkingHour(int id);
    Task UpdateWorkingHour(int id, WorkingHourDto workingHour);
}