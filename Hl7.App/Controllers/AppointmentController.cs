using Hl7.App.Dto;
using Hl7.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hl7.App.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly ISuiEncoder _encoder;

    public AppointmentController(ISuiEncoder encoder)
    {
        _encoder = encoder;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SendAppointment(string jsonText)
    {
        try
        {
            var message = JsonSerializer.Deserialize<AppointmentDto>(jsonText);

            var result = _encoder.Encode(message);

            //async database operations here... 
            await Task.CompletedTask;

            return Ok(result);
        }
        catch (JsonException) //just a sample - need to manage json exceptions separately
        {
            return BadRequest("Data could not be deserialized to json...");
        }
        catch (Exception)
        {
            return StatusCode(500, "Some error occured, see the logs...");
        }
    }
}
