using fastmed_api.DTO;

namespace fastmed_api.Services;

public interface IClinicCardService
{
    Task<List<ClinicCardDto>> GetClinicCards();
    Task<ClinicCardDto> GetClinicCard(int id);
    Task<ClinicCardDto> CreateClinicCard(ClinicCardDto card);
    Task DeleteClinicCard(int id);
    Task UpdateClinicCard(int id, ClinicCardDto card);
}