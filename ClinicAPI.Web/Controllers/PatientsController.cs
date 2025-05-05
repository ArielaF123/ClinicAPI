using Microsoft.AspNetCore.Mvc;
using ClinicAPI.PatientManager.Models;
using ClinicAPI.PatientManager.Services;
using Microsoft.Extensions.Logging;

namespace ClinicAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los pacientes");
                var patients = _patientService.GetAll();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los pacientes");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud");
            }
        }

        [HttpGet("{ci}")]
        public IActionResult GetByCI(string ci)
        {
            try
            {
                _logger.LogInformation($"Buscando paciente con CI: {ci}");
                var patient = _patientService.GetByCI(ci);
                if (patient == null)
                {
                    _logger.LogWarning($"Paciente con CI {ci} no encontrado");
                    return NotFound("Patient not found");
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar paciente con CI: {ci}");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Patient patient)
        {
            try
            {
                _logger.LogInformation($"Intentando crear paciente con CI: {patient.CI}");
                if (!_patientService.Create(patient))
                {
                    _logger.LogWarning($"Intento de crear paciente con CI existente: {patient.CI}");
                    return BadRequest("Ya existe un paciente con este CI");
                }
                return CreatedAtAction(nameof(GetByCI), new { ci = patient.CI }, patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear paciente con CI: {patient.CI}");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud");
            }
        }

        [HttpPut("{ci}")]
        public IActionResult Update(string ci, [FromBody] Patient patient)
        {
            try
            {
                _logger.LogInformation($"Intentando actualizar paciente con CI: {ci}");
                if (!_patientService.Update(ci, patient))
                {
                    _logger.LogWarning($"Intento de actualizar paciente no existente con CI: {ci}");
                    return NotFound("Patient not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar paciente con CI: {ci}");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud");
            }
        }

        [HttpDelete("{ci}")]
        public IActionResult Delete(string ci)
        {
            try
            {
                _logger.LogInformation($"Intentando eliminar paciente con CI: {ci}");
                if (!_patientService.Delete(ci))
                {
                    _logger.LogWarning($"Intento de eliminar paciente no existente con CI: {ci}");
                    return NotFound("Patient not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar paciente con CI: {ci}");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud");
            }
        }
    }
}