using AutoMapper;
using fastmed_api.DTO;
using fastmed_api.Models;
using fastmed_api.Repositories;

namespace fastmed_api.Services
{
    public class ClinicCardService : IClinicCardService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IMapper _mapper;

        public ClinicCardService(IClinicRepository clinicRepository, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }

        public async Task<List<ClinicCardDto>> GetClinicCards()
        {
            var clinics = await _clinicRepository.GetAllClinicCardsAsync();
            return _mapper.Map<List<ClinicCardDto>>(clinics);
        }

        public async Task<ClinicCardDto> GetClinicCard(int id)
        {
            var clinic = await _clinicRepository.GetClinicCardByIdAsync(id);
            if (clinic == null)
                throw new Exception($"ClinicCardService: Clinic with id {id} not found");

            return _mapper.Map<ClinicCardDto>(clinic);
        }

        public async Task<WorkingHourDto> GetWorkingHoursByWeekDay(int clinicId, int weekDay)
        {
            var clinic = await _clinicRepository.GetClinicCardByIdAsync(clinicId);
            if (clinic == null)
                throw new Exception($"ClinicCardService: Clinic with id {clinicId} not found");
            
            var mappedClinic = _mapper.Map<ClinicCardDto>(clinic);
            
            return mappedClinic.WorkingHours.Find(c => c.DayOfWeek == weekDay) ?? throw new Exception($"ClinicCardService: WorkingHour for week day {weekDay} not found");
        }

        public async Task<ClinicCardDto> CreateClinicCard(ClinicCardDto cardDto)
        {
            var clinic = _mapper.Map<ClinicCard>(cardDto);
            await _clinicRepository.AddClinicCardAsync(clinic);
            return _mapper.Map<ClinicCardDto>(clinic);
        }

        public async Task DeleteClinicCard(int id)
        {
            var clinic = await _clinicRepository.GetClinicCardByIdAsync(id);
            if (clinic == null)
                throw new Exception($"ClinicCardService: Clinic with id {id} not found");

            await _clinicRepository.DeleteClinicCardAsync(id);
        }

        public async Task UpdateClinicCard(int id, ClinicCardDto cardDto)
        {
            var clinic = await _clinicRepository.GetClinicCardByIdAsync(id);
            if (clinic == null)
                throw new Exception($"ClinicCardService: Clinic with id {id} not found");

            _mapper.Map(cardDto, clinic);
            await _clinicRepository.UpdateClinicCardAsync(clinic);
        }
    }
}