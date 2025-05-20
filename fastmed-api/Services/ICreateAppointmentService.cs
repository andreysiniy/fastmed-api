using fastmed_api.DTO;
using fastmed_api.Models;

namespace fastmed_api.Services;

public interface ICreateAppointmentService
{
    public Task<CreateAppointmentResultDto> CreateAppointment(CreateAppointmentDto createAppointmentDto);
   // public Task<CreateAppointmentResultDto> UpdateAppointment(int id, CreateAppointmentDto createAppointmentDto);
}