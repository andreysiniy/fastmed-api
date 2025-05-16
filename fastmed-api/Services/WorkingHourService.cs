using AutoMapper;
using fastmed_api.DTO;
using fastmed_api.Models;
using fastmed_api.Repositories;

namespace fastmed_api.Services
{
    public class WorkingHourService : IWorkingHourService
    {
        private readonly IWorkingHourRepository _workingHourRepository;
        private readonly IMapper _mapper;

        public WorkingHourService(IWorkingHourRepository workingHourRepository, IMapper mapper)
        {
            _workingHourRepository = workingHourRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkingHourDto>> GetWorkingHours()
        {
            var workingHours = await _workingHourRepository.GetAllWorkingHoursAsync();
            return _mapper.Map<List<WorkingHourDto>>(workingHours);
        }

        public async Task<WorkingHourDto> GetWorkingHour(int id)
        {
            var workingHour = await _workingHourRepository.GetWorkingHourByIdAsync(id);
            if (workingHour == null)
                throw new Exception($"WorkingHour with id {id} not found");

            return _mapper.Map<WorkingHourDto>(workingHour);
        }

        public async Task<WorkingHourDto> CreateWorkingHour(WorkingHourDto workingHourDto)
        {
            var workingHour = _mapper.Map<WorkingHour>(workingHourDto);
            await _workingHourRepository.AddWorkingHourAsync(workingHour);
            return _mapper.Map<WorkingHourDto>(workingHour);
        }

        public async Task DeleteWorkingHour(int id)
        {
            var workingHour = await _workingHourRepository.GetWorkingHourByIdAsync(id);
            if (workingHour == null)
                throw new Exception($"WorkingHour with id {id} not found");

            await _workingHourRepository.DeleteWorkingHourAsync(id);
        }

        public async Task UpdateWorkingHour(int id, WorkingHourDto workingHourDto)
        {
            var existing = await _workingHourRepository.GetWorkingHourByIdAsync(id);
            if (existing == null)
                throw new Exception($"WorkingHour with id {id} not found");

            _mapper.Map(workingHourDto, existing);
            await _workingHourRepository.UpdateWorkingHourAsync(existing);
        }
    }
}
