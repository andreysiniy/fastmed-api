using fastmed_api.Models;

namespace fastmed_api.Repositories;

public interface IDoctorRepository
{
    Task<IEnumerable<DoctorCard>> GetDoctorCardsAsync();
    Task<DoctorCard> GetDoctorCardAsync(int id);
    Task AddDoctorCardAsync(DoctorCard doctorCard);
    Task UpdateDoctorCardAsync(DoctorCard doctorCard);
    Task DeleteDoctorCardAsync(int doctorCardId);
    Task<IEnumerable<DoctorCard>> GetDoctorCardsByClinicIdAsync(int clinicId);
}