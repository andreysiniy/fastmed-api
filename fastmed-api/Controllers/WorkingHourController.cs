using fastmed_api.DTO;
using fastmed_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace fastmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkingHourController : ControllerBase
    {
        private readonly IWorkingHourService _workingHourService;
        private readonly ILogger<WorkingHourController> _logger;

        public WorkingHourController(IWorkingHourService workingHourService, ILogger<WorkingHourController> logger)
        {
            _workingHourService = workingHourService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkingHourDto>>> GetAll()
        {
            _logger.LogInformation("Getting all working hours");
            var workingHours = await _workingHourService.GetWorkingHours();
            _logger.LogInformation($"Returning {workingHours.Count} working hours");
            return Ok(workingHours);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkingHourDto>> GetById(int id)
        {
            _logger.LogInformation($"Getting working hour with id {id}");
            var workingHour = await _workingHourService.GetWorkingHour(id);
            if (workingHour == null)
            {
                _logger.LogWarning($"Working hour with id {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Returning working hour with id {id}");
            return Ok(workingHour);
        }

        [HttpPost]
        public async Task<ActionResult<WorkingHourDto>> Create([FromBody] WorkingHourDto workingHourDto)
        {
            _logger.LogInformation($"Creating working hour for on day {workingHourDto.DayOfWeek}");
            var created = await _workingHourService.CreateWorkingHour(workingHourDto);
            _logger.LogInformation($"Created working hour with id {created.Id}");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkingHourDto workingHourDto)
        {
            _logger.LogInformation($"Updating working hour with id {id}");
            if (id != workingHourDto.Id)
            {
                _logger.LogWarning($"Working hour id mismatch: route id {id}, body id {workingHourDto.Id}");
                return BadRequest("WorkingHour ID mismatch");
            }

            await _workingHourService.UpdateWorkingHour(id, workingHourDto);
            _logger.LogInformation($"Updated working hour with id {id}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting working hour with id {id}");
            await _workingHourService.DeleteWorkingHour(id);
            _logger.LogInformation($"Deleted working hour with id {id}");
            return Ok();
        }
    }
}
