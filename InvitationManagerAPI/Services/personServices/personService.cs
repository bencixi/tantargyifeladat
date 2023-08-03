using AutoMapper;
using InvitationManagerAPI.Data;
using InvitationManagerAPI.Dtos.User;
using InvitationManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InvitationManagerAPI.Services.personServices
{
    public class personService : IPersonService
    {
        private static List<Person> persons = new List<Person>
        {
           
        };
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public personService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetPersonDto>>> AddPerson(AddPersonDto newPerson)
        {
            var serviceResponse = new ServiceResponse<List<GetPersonDto>>();
            var dbPerson = _mapper.Map<Person>(newPerson);         
            _context.Persons.Add(dbPerson);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _context.Persons.Select(x => _mapper.Map<GetPersonDto>(x)).ToList();        
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> DeletePerson(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetPersonDto>>();
            try
            {
                var dbPerson = await _context.Persons.FirstOrDefaultAsync(c => c.Id == id);
                if (dbPerson == null)               
                    throw new Exception($"Felhasználó '{id}' Id val nem található");              
                _context.Persons.Remove(dbPerson);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> GetAllPerson()
        {
            var serviceResponse = new ServiceResponse<List<GetPersonDto>>();
            var dbPersons = await _context.Persons.ToListAsync();
            serviceResponse.Data = dbPersons.Select(x => _mapper.Map<GetPersonDto>(x)).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetPersonDto>> GetPersonById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPersonDto>();
            var dbPerson = await _context.Persons.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetPersonDto>(dbPerson);
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<GetPersonDto>> UpdatePerson(UpdatePersonDto updatedPerson)
        {
            var serviceResponse = new ServiceResponse<GetPersonDto>();
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(person => person.Id == updatedPerson.Id);
                if(person == null) 
                {
                    throw new Exception($"Felhasználó '{updatedPerson.Id}' Id val nem található");
                };
                person.name = updatedPerson.name;
                person.email = updatedPerson.email;
                person.telefonszam = updatedPerson.telefonszam;
                person.erzekenysegek = updatedPerson.erzekenysegek;
                person.vallas = updatedPerson.vallas;
                person.fogyatekossag = updatedPerson.fogyatekossag;
                person.titulus = updatedPerson.titulus;
                person.userType = updatedPerson.userType;
                person.ProfilePic = updatedPerson.ProfilePic;
                person.utolsoesemeny = updatedPerson.utolsoesemeny;
                serviceResponse.Data = _mapper.Map<GetPersonDto>(person);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;               
            }
            return serviceResponse;

        }
    }
}
