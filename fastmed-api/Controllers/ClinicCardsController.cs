using fastmed_api.DTO;
using fastmed_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace fastmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicCardsController : ControllerBase
    {
        private readonly IClinicCardService _clinicService;
        private readonly ILogger<ClinicCardsController> _logger;

        public ClinicCardsController(IClinicCardService clinicService, ILogger<ClinicCardsController> logger)
        {
            _clinicService = clinicService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClinicCardDto>>> GetAll()
        {
            _logger.LogInformation("Getting all clinic cards");
            var clinics = await _clinicService.GetClinicCards();
            _logger.LogInformation($"Returning all clinic cards from {clinics.Count} clinics: {string.Join(", ", clinics)}");
            return Ok(clinics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicCardDto>> GetById(int id)
        {
            _logger.LogInformation($"Getting clinic card with id {id}");
            var clinic = await _clinicService.GetClinicCard(id);
            if (clinic == null) return NotFound();
            _logger.LogInformation($"Returning clinic card with id {id}: {clinic}");
            return Ok(clinic);
        }
        
        [HttpGet("name/{name}")]
        public async Task<ActionResult<ClinicCardDto>> GetByName(string name)
        {
            _logger.LogInformation($"Getting clinic card with name {name}");
            var clinic = await _clinicService.GetClinicCardsByName(name);
            _logger.LogInformation($"Returning clinic card with name {name}: {clinic}");
            return Ok(clinic);
        }


        [HttpPost]
        public async Task<ActionResult<ClinicCardDto>> Create(ClinicCardDto clinicCardDto)
        {
            _logger.LogInformation($"Creating clinic card: {clinicCardDto}");
            var created = await _clinicService.CreateClinicCard(clinicCardDto);
            _logger.LogInformation($"Returning created clinic card {created}");
            return CreatedAtAction(nameof(GetById), new { id = created.ClinicId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClinicCardDto clinicCardDto)
        {
            _logger.LogInformation($"Updating clinic card with id {id}: {clinicCardDto}");
            if (id != clinicCardDto.ClinicId) return BadRequest();
            await _clinicService.UpdateClinicCard(id, clinicCardDto);
            _logger.LogInformation($"Updated clinic card with id: {id}: {clinicCardDto}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting clinic card with id {id}");
            await _clinicService.DeleteClinicCard(id);
            _logger.LogInformation($"Deleted clinic card with id: {id}");
            return Ok();
        }
    }
}