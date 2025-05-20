using fastmed_api.DTO;
using fastmed_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace fastmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ICreateAppointmentService _createAppointmentService;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, ICreateAppointmentService createAppointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _createAppointmentService = createAppointmentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDto>>> GetAll()
        {
            _logger.LogInformation("Getting all appointments");
            var appointments = await _appointmentService.GetAppointments();
            _logger.LogInformation($"Returning {appointments.Count} appointments");
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById(int id)
        {
            _logger.LogInformation($"Getting appointment with id {id}");
            var appointment = await _appointmentService.GetAppointment(id);
            if (appointment == null)
            {
                _logger.LogWarning($"Appointment with id {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Returning appointment with id {id}");
            return Ok(appointment);
        }

        [HttpPost]
        [Route("createOld")]
        public async Task<ActionResult<AppointmentDto>> Create([FromBody] AppointmentDto appointmentDto)
        {
            _logger.LogInformation($"Creating appointment for patient {appointmentDto.PatientName}");
            var created = await _appointmentService.CreateAppointment(appointmentDto);
            _logger.LogInformation($"Created appointment with id {created.AppointmentId}");
            return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId }, created);
        }
        
        [HttpPost]
        public async Task<ActionResult<CreateAppointmentResultDto>> Create([FromBody] CreateAppointmentDto appointmentDto)
        {
            _logger.LogInformation($"Creating appointment for patient {appointmentDto.PatientName}");
            var created = await _createAppointmentService.CreateAppointment(appointmentDto);
            _logger.LogInformation($"Created appointment with id {created.AppointmentId}");
            return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentDto appointmentDto)
        {
            _logger.LogInformation($"Updating appointment with id {id}");
            if (id != appointmentDto.AppointmentId)
            {
                _logger.LogWarning($"Appointment id mismatch: route id {id}, body id {appointmentDto.AppointmentId}");
                return BadRequest("Appointment ID mismatch");
            }

            await _appointmentService.UpdateAppointment(id, appointmentDto);
            _logger.LogInformation($"Updated appointment with id {id}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting appointment with id {id}");
            await _appointmentService.DeleteAppointment(id);
            _logger.LogInformation($"Deleted appointment with id {id}");
            return Ok();
        }
    }
}
