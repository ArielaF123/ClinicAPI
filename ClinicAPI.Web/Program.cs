using ClinicAPI.PatientManager.Services;
using ClinicAPI.PatientManager.Models;
using Microsoft.OpenApi.Models; // Añade este using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración detallada de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clinic API",
        Version = "v1",
        Description = "API para gestión de pacientes de una clínica",
        Contact = new OpenApiContact
        {
            Name = "Tu Nombre",
            Email = "tu@email.com"
        }
    });
});

// Configurar HttpClientFactory
builder.Services.AddHttpClient();

// Configurar ruta del archivo de pacientes
var patientsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "patients.txt");
builder.Services.AddSingleton<IPatientService>(new PatientFileService(patientsFilePath));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();