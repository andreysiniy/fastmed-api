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

        // ClinicCard → ClinicCardDto
        CreateMap<ClinicCard, ClinicCardDto>();

        // WorkingHour → WorkingHourDto
        CreateMap<WorkingHour, WorkingHourDto>();

        // Appointment → AppointmentDto
        CreateMap<Appointment, AppointmentDto>();
        // AppointmentDto → Appointment
        CreateMap<AppointmentDto, Appointment>();
    }
}