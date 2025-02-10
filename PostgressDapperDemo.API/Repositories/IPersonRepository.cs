using PostgressDapperDemo.API.Models.Domain;
using PostgressDapperDemo.API.Models.DTOs;

namespace PostgressDapperDemo.API.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> CreatePersonAsync(Person personCreateDto);
        Task<Person> UpdatePersonAsync(Person personUpdateDto);
        Task DeletePersonAsync(int id);
        Task<IEnumerable<Person>> GetAllPersonAsync();
        Task<Person> GetPersonByIdAsync(int id);

    }
}
