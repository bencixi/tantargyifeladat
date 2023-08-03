using InvitationManagerAPI.Dtos.User;
using InvitationManagerAPI.Models;
using InvitationManagerAPI.Services.personServices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InvitationManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private static List<Person> persons = new List<Person>
        {
            new Person(),
        };
        private readonly IPersonService _PersonService;

        public PersonController(IPersonService PersonService)
        {
            _PersonService = PersonService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetPersonDto>>>> Get()
        {
            return Ok(await _PersonService.GetAllPerson());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPersonDto>>> GetSingle(int id)
        {
            return Ok(await _PersonService.GetPersonById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPersonDto>>>> AddPerson(AddPersonDto newPerson)
        {        
            return Ok(await _PersonService.AddPerson(newPerson));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetPersonDto>>>> UpdatePerson(UpdatePersonDto updatePersonDto)
        {
            var response = await _PersonService.UpdatePerson(updatePersonDto);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPersonDto>>> DeletePerson(int id)
        {
            var response = await _PersonService.DeletePerson(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

