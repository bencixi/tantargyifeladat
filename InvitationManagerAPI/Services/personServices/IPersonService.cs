using InvitationManagerAPI.Dtos.User;
using InvitationManagerAPI.Models;

namespace InvitationManagerAPI.Services.personServices
{
    public interface IPersonService
    {
        Task<ServiceResponse<List<GetPersonDto>>> GetAllPerson();
        Task<ServiceResponse<GetPersonDto>> GetPersonById(int id);
        Task<ServiceResponse<List<GetPersonDto>>> AddPerson(AddPersonDto newPerson);
        Task<ServiceResponse<GetPersonDto>> UpdatePerson(UpdatePersonDto updatedPerson);

        Task<ServiceResponse<List<GetPersonDto>>> DeletePerson(int id);


    }
}
