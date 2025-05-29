using fastmed_api.DTO;
using fastmed_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace fastmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorCardsController : ControllerBase
    {
        private readonly IDoctorCardService _doctorCardService;
        private readonly ILogger<DoctorCardsController> _logger;

        public DoctorCardsController(IDoctorCardService doctorCardService, ILogger<DoctorCardsController> logger)
        {
            _doctorCardService = doctorCardService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorCardDto>>> GetAll()
        {
            _logger.LogInformation("Getting all doctor cards");
            var doctors = await _doctorCardService.GetDoctorCards();
            _logger.LogInformation($"Returning {doctors.Count} doctor cards");
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorCardDto>> GetById(int id)
        {
            _logger.LogInformation($"Getting doctor card with id {id}");
            var doctor = await _doctorCardService.GetDoctorCard(id);
            if (doctor == null)
            {
                _logger.LogWarning($"Doctor card with id {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Returning doctor card with id {id}");
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorCardDto>> Create([FromBody] DoctorCardDto doctorCardDto)
        {
            _logger.LogInformation($"Creating doctor card: {doctorCardDto.Name}");
            var created = await _doctorCardService.CreateDoctorCard(doctorCardDto);
            _logger.LogInformation($"Created doctor card with id {created.DoctorId}");
            return CreatedAtAction(nameof(GetById), new { id = created.DoctorId }, created);
        }
        
        [HttpGet("speciality/{speciality}")]
        public async Task<ActionResult<List<DoctorCardDto>>> GetBySpeciality(string speciality)
        {
            _logger.LogInformation($"Attempting to get doctor cards with speciality: {speciality}");
            var doctors = await _doctorCardService.GetDoctorCardsBySpecialityAsync(speciality);
            
            if (doctors == null)
            {
                _logger.LogInformation($"No doctor cards found with speciality: {speciality}");
                return NotFound($"No doctors found with speciality: {speciality}");
            }
            
            _logger.LogInformation($"Returning {doctors.Count} doctor cards with speciality {speciality}");
            return Ok(doctors);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorCardDto doctorCardDto)
        {
            _logger.LogInformation($"Updating doctor card with id {id}");
            if (id != doctorCardDto.DoctorId)
            {
                _logger.LogWarning($"Doctor card id mismatch: route id {id}, body id {doctorCardDto.DoctorId}");
                return BadRequest("Doctor ID mismatch");
            }

            await _doctorCardService.UpdateDoctorCard(id, doctorCardDto);
            _logger.LogInformation($"Updated doctor card with id {id}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting doctor card with id {id}");
            await _doctorCardService.DeleteDoctorCard(id);
            _logger.LogInformation($"Deleted doctor card with id {id}");
            return Ok();
        }
    }
}
