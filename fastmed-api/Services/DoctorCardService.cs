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

        public async Task<List<DoctorCardDto>> GetDoctorCards(
            int? clinicId, 
            string? speciality, 
            string? name, 
            DateTime? appointmentDate)
        {
            var doctorsFromRepo = await _doctorRepository.GetFilteredDoctorCardsAsync(clinicId, speciality, name);
            var filteredDoctorDtos = _mapper.Map<List<DoctorCardDto>>(doctorsFromRepo);
            if (appointmentDate.HasValue)
            {
                    var availableAtTimeDoctors = new List<DoctorCardDto>();
                    TimeSpan requestedTimeOfDay = appointmentDate.Value.TimeOfDay;
                    var tasks = filteredDoctorDtos.Select(async doctorDto =>
                    {
                        List<TimeSpan> doctorAvailableSlots = await GetAvailableHoursAsync(doctorDto.DoctorId, appointmentDate.Value);
                        
                        if (doctorAvailableSlots.Contains(requestedTimeOfDay))
                        {
                            return doctorDto;
                        }
                    
                        return null; 
                }).ToList();

                var results = await Task.WhenAll(tasks);
                availableAtTimeDoctors.AddRange(results.Where(d => d != null)!); 
                
                return availableAtTimeDoctors;
            }

            return filteredDoctorDtos;
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
        
        
        public async Task<List<TimeSpan>> GetAvailableHoursAsync(int doctorCardId, DateTime date)
        {
            int weekDay = (int)date.DayOfWeek;
            var doctor = await _doctorRepository.GetDoctorCardAsync(doctorCardId);
            var doctorWorkingHours = doctor.Clinic.WorkingHours;
            var workingHourEntity = doctorWorkingHours.Where(c => c.DayOfWeek == weekDay).FirstOrDefault();
            WorkingHourDto weekDayWorkingHours = _mapper.Map<WorkingHourDto>(workingHourEntity);
            var timeSlots = new List<TimeSpan>();
    
            var currentTime = weekDayWorkingHours.OpenTime;
            while (currentTime < weekDayWorkingHours.CloseTime)
            {
                timeSlots.Add(currentTime);
                currentTime = currentTime.Add(TimeSpan.FromMinutes(30)); 
            }

            return timeSlots;
        }

      /*  public async Task<List<DateTime>> GetAvailableDatesFromTodayAsync(int doctorCardId, DateTime fromDate)
        {
            int weekDay = (int)fromDate.DayOfWeek;
            var doctor = await _doctorRepository.GetDoctorCardAsync(doctorCardId);
            var doctorWorkingHours = doctor.Clinic.WorkingHours;
        }

       */
        public async Task<List<DoctorCardDto>> GetDoctorCardsByNameAsync(string name)
        {
            return _mapper.Map<List<DoctorCardDto>>(await _doctorRepository.GetDoctorCardsByNameAsync(name));
        }

     //   public async Task<List<DoctorCardDto>> GetDoctorCardsByClinic(string clinicName)
      //  {
       // } 
    }
}
