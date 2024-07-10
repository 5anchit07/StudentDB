using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoCrud.API.Mapper;
using MongoCrud.API.Models.Domain;
using MongoCrud.API.Models.DTO;
using MongoCrud.API.Repository;
using MongoDB.Bson;

namespace MongoCrud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepo;
        public StudentController(IMapper mapper, IStudentRepository studentRepo)
        {
            _mapper = mapper;
            _studentRepo = studentRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto studentDto)
        {
            // dto to model
            var studentModel = _mapper.Map<Student>(studentDto);

            studentModel = await _studentRepo.CreateAsync(studentModel);

            return Ok(studentModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var studentModel = await _studentRepo.GetAllAsync();
            return Ok(studentModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var existingModel = await _studentRepo.GetByIdAsync(id);
            if (existingModel == null)
            {
                return NotFound("not found");
            }
            var studentDto = _mapper.Map<AddStudentRequestDto>(existingModel);
            return Ok(studentDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateStudentRequestDto studentDto)
        {
            // dto to model
            var studentModel = _mapper.Map<Student>(studentDto);
            studentModel = await _studentRepo.UpdateAsync(id, studentModel);
            if (studentModel == null) { return NotFound("student with the given id is not found"); }
            //return addstudentdto
            var studDto = _mapper.Map<AddStudentRequestDto>(studentModel);
            return Ok(studDto);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var stockModel = await _studentRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound("object with given id not found");
            }
            return Ok(stockModel);
        }

    }
}
