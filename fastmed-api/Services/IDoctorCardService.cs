using fastmed_api.DTO;

namespace fastmed_api.Services;

public interface IDoctorCardService
{
    Task<List<DoctorCardDto>> GetDoctorCards();
    Task<DoctorCardDto> GetDoctorCard(int id);
    Task<DoctorCardDto> CreateDoctorCard(DoctorCardDto card);
    Task DeleteDoctorCard(int id);
    Task UpdateDoctorCard(int id, DoctorCardDto card);
    Task<List<DoctorCardDto>> GetDoctorCardsBySpecialityAsync(string speciality);
    
}