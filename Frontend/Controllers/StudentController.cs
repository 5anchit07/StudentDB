using Frontend.Models;
using Frontend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Frontend.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public StudentController(IHttpClientFactory httpClientFactory)
        {
             _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<StudentDto> response = new List<StudentDto>();
            try
            {
                //get data from api
                var client = _httpClientFactory.CreateClient();
                // we will call the method from the api project

                var httpResponseMessage = await client.GetAsync("https://localhost:7060/api/student");
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<StudentDto>>());
                
            }
            catch (Exception)
            {

                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7060/api/student"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response=await httpResponseMessage.Content.ReadFromJsonAsync<StudentDto>();
            if(response is not null)
            {
                return RedirectToAction("Index", "Student");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response=await client.GetFromJsonAsync<StudentDto>($"https://localhost:7060/api/student/{id.ToString()}");
            if(response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7060/api/student/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode() ;

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<StudentDto>();
            if (response is not null)
            {
                return RedirectToAction("Index", "Student");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7060/api/student/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Student");
            }
            catch (Exception ex)
            {

                
            }
            return View("Edit");
        }
    }
}
