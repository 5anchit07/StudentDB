using AutoMapper;
using MongoCrud.API.Models.Domain;
using MongoCrud.API.Models.DTO;
using MongoDB.Bson;

namespace MongoCrud.API.Mapper
{
    public class StudentAutoMapper:Profile
    {
        public StudentAutoMapper()
        {
            CreateMap<Student, AddStudentRequestDto>().ReverseMap().ForMember(dest => dest.Id, options => options.MapFrom(src => ObjectId.GenerateNewId().ToString()));
            CreateMap<Student, UpdateStudentRequestDto>().ReverseMap();
        }
    }
}
