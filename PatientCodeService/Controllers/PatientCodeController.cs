using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/patientcode")]
public class PatientCodeController : ControllerBase
{
    [HttpGet("generate")]
    public IActionResult GenerateCode([FromQuery] string name, [FromQuery] string lastName, [FromQuery] string ci)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(ci))
            return BadRequest("Nombre, Apellido y CI son requeridos.");

        var code = $"{name[0]}{lastName[0]}-{ci}".ToUpper(); // Ejemplo: "MT-123456"
        return Ok(code);
    }
}