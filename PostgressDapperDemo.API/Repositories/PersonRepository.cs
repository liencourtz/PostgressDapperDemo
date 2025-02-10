using Dapper;
using Npgsql;
using PostgressDapperDemo.API.Models.Domain;
using PostgressDapperDemo.API.Models.DTOs;
using System.Data;

namespace PostgressDapperDemo.API.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private IDbConnection _connection;

        public PersonRepository(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            var createdID = await _connection.ExecuteScalarAsync<int>("INSERT INTO person (name, email) VALUES (@name,@email); SELECT LASTVAL();", person);
            person.Id = createdID;
            return person;
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            await _connection.ExecuteAsync("UPDATE person SET name = @name, email = @email WHERE id = @id", person);
            return person;
        }

        public async Task DeletePersonAsync(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM person WHERE id = @id", new { id });
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            return await _connection.QueryAsync<Person>("SELECT * FROM person");
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Person>("SELECT * FROM person WHERE id = @id", new { id });
        }
    }
}
