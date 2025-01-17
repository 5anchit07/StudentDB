using MongoCrud.API.Models.Domain;
using MongoCrud.API.Repository;
using TT.Mongo.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongoDbClient("mongodb://localhost:27017");
builder.Services.AddScoped<IMongoDbClient<Student>>(option =>
{
    return new MongoDbClient<Student>("CollegeDb", "Students");
});
builder.Services.AddAutoMapper(typeof(Student));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
