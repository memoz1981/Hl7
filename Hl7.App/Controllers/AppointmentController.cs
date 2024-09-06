using Hl7.App.Dto;
using Hl7.App.Services;
using Microsoft.AspNetCore.Mvc;
using NHapi.Base;
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
    public async Task<IActionResult> SendAppointment(AppointmentDto appointment)
    {
        try
        {
            var result = _encoder.Encode(appointment);

            //async database operations here... 
            await Task.CompletedTask;

            return Ok(result);
        }
        catch (HL7Exception)
        {
            return BadRequest("Data could not be deserialized to json...");
        }
        catch (JsonException) //just a sample - need to manage json exceptions separately
        {
            return BadRequest("Data could not be deserialized to json...");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Some error occured, {ex.Message}");
        }
    }
}
