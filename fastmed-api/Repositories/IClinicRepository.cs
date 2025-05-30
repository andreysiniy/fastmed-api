using fastmed_api.Models;

namespace fastmed_api.Repositories;

public interface IClinicRepository
{
    Task<IEnumerable<ClinicCard>> GetAllClinicCardsAsync();
    Task<ClinicCard> GetClinicCardByIdAsync(int id);
    Task<IEnumerable<ClinicCard>> GetClinicCardsByNameAsync(string name);
    Task AddClinicCardAsync(ClinicCard clinicCard);
    Task UpdateClinicCardAsync(ClinicCard clinicCard);
    Task DeleteClinicCardAsync(int clinicCardId);
    
}