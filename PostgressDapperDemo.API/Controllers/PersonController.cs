using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostgressDapperDemo.API.Models.Domain;
using PostgressDapperDemo.API.Models.DTOs;
using PostgressDapperDemo.API.Repositories;

namespace PostgressDapperDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonRepository personRepository, IMapper mapper, ILogger<PersonController> logger)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<IActionResult> GetPerson(int id)
        {
            try
            {
                var person = await _personRepository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    return NotFound();
                }
                var personDto = _mapper.Map<PersonDisplayDto>(person);
                return Ok(personDto);
            }catch(Exception ex)
            {
                _logger.LogError($"Error getting person: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonCreateDto personCreateDto)
        {
            try
            {
                var person = await _personRepository.CreatePersonAsync(_mapper.Map<Person>(personCreateDto));
                return CreatedAtRoute(nameof(GetPerson), new { id = person.Id }, person);
            }catch(Exception ex)
            {
                _logger.LogError($"Error creating person: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonUpdateDto personUpdateDto)
        {
            try
            {
                if( id != personUpdateDto.Id)
                {
                    return BadRequest($"Person ID {personUpdateDto.Id} mismatch");
                }
                var existingPerson = await _personRepository.GetPersonByIdAsync(id);
                if ( existingPerson == null)
                {
                    return NotFound();
                }
                var person = await _personRepository.UpdatePersonAsync(_mapper.Map<Person>(personUpdateDto));
                return NoContent();
            }catch (Exception ex)
            {
                _logger.LogError($"Error updating person: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var existingPerson = await _personRepository.GetPersonByIdAsync(id);
                if (existingPerson == null)
                {
                    return NotFound();
                }
                await _personRepository.DeletePersonAsync(id);
                return NoContent(); //TODO: Check if this is the correct return type
            }catch(Exception ex)
            {
                _logger.LogError($"Error deleting person: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPerson()
        {
            try
            {
                var persons = await _personRepository.GetAllPersonAsync();
                if(persons == null)
                {
                    return NotFound();
                }
                return Ok(persons);
            }catch(Exception ex)
            {
                _logger.LogError($"Error getting all people: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
