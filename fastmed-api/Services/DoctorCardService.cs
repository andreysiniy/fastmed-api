using AutoMapper;
using fastmed_api.DTO;
using fastmed_api.Models;
using fastmed_api.Repositories;

namespace fastmed_api.Services
{
    public class DoctorCardService : IDoctorCardService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorCardService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorCardDto>> GetDoctorCards()
        {
            var doctors = await _doctorRepository.GetDoctorCardsAsync();
            return _mapper.Map<List<DoctorCardDto>>(doctors);
        }

        public async Task<DoctorCardDto> GetDoctorCard(int id)
        {
            var doctor = await _doctorRepository.GetDoctorCardAsync(id);
            if (doctor == null)
                throw new Exception($"Doctor with id {id} not found");

            return _mapper.Map<DoctorCardDto>(doctor);
        }

        public async Task<DoctorCardDto> CreateDoctorCard(DoctorCardDto cardDto)
        {
            var doctor = _mapper.Map<DoctorCard>(cardDto);
            await _doctorRepository.AddDoctorCardAsync(doctor);
            return _mapper.Map<DoctorCardDto>(doctor);
        }

        public async Task DeleteDoctorCard(int id)
        {
            var doctor = await _doctorRepository.GetDoctorCardAsync(id);
            if (doctor == null)
                throw new Exception($"Doctor with id {id} not found");

            await _doctorRepository.DeleteDoctorCardAsync(id);
        }

        public async Task UpdateDoctorCard(int id, DoctorCardDto cardDto)
        {
            var existing = await _doctorRepository.GetDoctorCardAsync(id);
            if (existing == null)
                throw new Exception($"Doctor with id {id} not found");

            _mapper.Map(cardDto, existing);
            await _doctorRepository.UpdateDoctorCardAsync(existing);
        }
        
        public async Task<List<DoctorCardDto>> GetDoctorCardsBySpecialityAsync(string speciality)
        {
            var doctors = await _doctorRepository.GetDoctorCardsBySpecialityAsync(speciality);
            return _mapper.Map<List<DoctorCardDto>>(doctors);
        }
    }
}
