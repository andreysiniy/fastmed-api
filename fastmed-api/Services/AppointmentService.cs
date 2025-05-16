using AutoMapper;
using fastmed_api.DTO;
using fastmed_api.Models;
using fastmed_api.Repositories; 


namespace fastmed_api.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository; 
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> GetAppointment(int id)
    {
        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
        if (appointment == null) throw new Exception($"AppointmentService: Appointment with id {id} not found");
        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<List<AppointmentDto>> GetAppointments()
    {
        var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
        return _mapper.Map<List<AppointmentDto>>(appointments);
    }

    public async Task<AppointmentDto> CreateAppointment(AppointmentDto appointmentDto)
    {
        var appointment = _mapper.Map<Appointment>(appointmentDto);
        await _appointmentRepository.AddAppointmentAsync(appointment);
        return _mapper.Map<AppointmentDto>(appointment);
    }
    
    public async Task DeleteAppointment(int id)
    {
        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
        if (appointment == null) throw new Exception($"AppointmentService: Appointment with id {id} not found");

        await _appointmentRepository.DeleteAppointmentAsync(id);
    }

    public async Task UpdateAppointment(int id, AppointmentDto appointmentDto)
    {
        var existing = await _appointmentRepository.GetAppointmentByIdAsync(id);
        if (existing == null) throw new Exception($"AppointmentService: Appointment with id {id} not found");

        _mapper.Map(appointmentDto, existing); 
        await _appointmentRepository.UpdateAppointmentAsync(existing);
    }
}