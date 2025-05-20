using AutoMapper;
using fastmed_api.DTO;
using fastmed_api.Models;
using fastmed_api.Repositories;
using fastmed_api.Misc;

namespace fastmed_api.Services;

public class CreateAppointmentService : ICreateAppointmentService
{
    private readonly IClinicCardService _clinicCardService;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public CreateAppointmentService(IClinicCardService clinicCardService, IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IMapper mapper)
    {
        _clinicCardService = clinicCardService;
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<CreateAppointmentResultDto> CreateAppointment(CreateAppointmentDto appointmentDto)
    {
        if (!appointmentDto.DoctorId.HasValue || appointmentDto.DoctorId.Value == 0)
        {
            throw new ArgumentException("A valid DoctorId must be provided.", nameof(appointmentDto.DoctorId));
        }
        var doctorEntity = await _doctorRepository.GetDoctorCardAsync(appointmentDto.DoctorId.Value);
        var clinicDto = await _clinicCardService.GetClinicCard(doctorEntity.ClinicId);
        var appointmentWeekDay = (int)appointmentDto.AppointmentTime.DayOfWeek;
        WorkingHourDto workingHours =
            await _clinicCardService.GetWorkingHoursByWeekDay(doctorEntity.ClinicId, appointmentWeekDay);

        if (!MedUtils.isInTimeRange(appointmentDto.AppointmentTime, workingHours.DayOfWeek, workingHours.OpenTime, workingHours.CloseTime))
            throw new ArgumentException("The provided time is not in the working hours of the clinic.", nameof(appointmentDto.AppointmentTime));
        
        var appointmentEntity = _mapper.Map<Appointment>(appointmentDto);
        appointmentEntity.Doctor = doctorEntity;
        
        await _appointmentRepository.AddAppointmentAsync(appointmentEntity);
        
        var appointmentDtoResult = _mapper.Map<CreateAppointmentResultDto>(appointmentEntity);
        appointmentDtoResult.ClinicName = clinicDto.Name;
        
        return appointmentDtoResult;
    }

}