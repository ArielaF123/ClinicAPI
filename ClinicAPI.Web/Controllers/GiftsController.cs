using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ClinicAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GiftsController> _logger;

        public GiftsController(IHttpClientFactory httpClientFactory, ILogger<GiftsController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGifts()
        {
            try
            {
                _logger.LogInformation("Obteniendo lista de regalos de la API externa");
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://api.restful-api.dev/objects");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al obtener regalos. Código de estado: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode, "Error al obtener datos de la API externa");
                }

                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener regalos de la API externa");
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud");
            }
        }
    }
}