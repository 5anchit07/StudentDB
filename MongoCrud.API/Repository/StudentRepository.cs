using Microsoft.AspNetCore.Http.HttpResults;
using MongoCrud.API.Models.Domain;
using MongoDB.Bson;
using TT.Mongo.Client;

namespace MongoCrud.API.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoDbClient<Student> _client;
        public StudentRepository(IMongoDbClient<Student> mongoDbClient) 
        {
            _client = mongoDbClient;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await _client.CreateAsync(student);
            return student;

        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _client.GetManyAsync();
        }

        public async Task<Student?> GetByIdAsync(string id)
        {
            return await _client.GetAsync(id);
        }

        public async Task<Student?> UpdateAsync(string id,Student studentModel)
        {
            var existingModel = await _client.GetAsync(id);
            if(existingModel == null)
            {
                return null;
            }
            // update
            existingModel.Address = studentModel.Address;
            existingModel.Name = studentModel.Name;
            existingModel.Email = studentModel.Email;
            existingModel.Phone = studentModel.Phone;

            await _client.UpdateAsync(id, existingModel);
            return existingModel;
        }

        public async Task<Student?> DeleteAsync(String id)
        {
            var existing = await _client.GetAsync(id);
            if (existing == null)
            {
                return null;
            }
            await _client.RemoveAsync(id);
            return existing;
        }
    }
}
