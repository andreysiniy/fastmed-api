using AutoMapper;
using fastmed_api.DTO;
using fastmed_api.Models;

namespace fastmed_api.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // DoctorCard → DoctorCardDto
        CreateMap<DoctorCard, DoctorCardDto>();
        
        // DoctorCardDto → DoctorCard
        CreateMap<DoctorCardDto, DoctorCard>();
        
        // ClinicCard → ClinicCardDto
        CreateMap<ClinicCard, ClinicCardDto>();

        // WorkingHour → WorkingHourDto
        CreateMap<WorkingHour, WorkingHourDto>();
        
        // WorkingHourDto → WorkingHour 
        CreateMap<WorkingHourDto, WorkingHour>();

        // Appointment → AppointmentDto
        CreateMap<Appointment, AppointmentDto>();
        // AppointmentDto → Appointment
        CreateMap<AppointmentDto, Appointment>();
        
        // CreateAppointmentDto → Appointment
        CreateMap<CreateAppointmentDto, Appointment>()
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId ?? 0)) // Если `DoctorId` null, подставить 0
            .ForMember(dest => dest.Doctor, opt => opt.Ignore()); // Поле Doctor заполняется вручную

        // Appointment → CreateAppointmentResultDto
        CreateMap<Appointment, CreateAppointmentResultDto>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name))
            .ForMember(dest => dest.DoctorSpeciality, opt => opt.MapFrom(src => src.Doctor.Speciality))
            .ForMember(dest => dest.ClinicName, opt => opt.Ignore()); // ClinicName будет заполняться вручную

    }
}