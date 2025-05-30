using fastmed_api.DTO;

namespace fastmed_api.Services;

public interface IDoctorCardService
{
    Task<List<DoctorCardDto>> GetDoctorCards(int? clinicId, string? speciality, string? name, DateTime? appointmentDate);
    Task<DoctorCardDto> GetDoctorCard(int id);
    Task<DoctorCardDto> CreateDoctorCard(DoctorCardDto card);
    Task DeleteDoctorCard(int id);
    Task UpdateDoctorCard(int id, DoctorCardDto card);
    Task<List<DoctorCardDto>> GetDoctorCardsBySpecialityAsync(string speciality);
    Task<List<TimeSpan>> GetAvailableHoursAsync(int doctorCardId, DateTime date);
  //  Task<List<DateTime>> GetAvailableDatesFromTodayAsync(int doctorCardId, DateTime fromDate);
    Task<List<DoctorCardDto>> GetDoctorCardsByNameAsync(string name);
   // Task<List<DoctorCardDto>> GetDoctorCardsByClinic(string clinicName);
}