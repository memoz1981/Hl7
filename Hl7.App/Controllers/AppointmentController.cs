using AutoMapper;
using Hl7.App.Dto;
using Hl7.App.Services;
using Hl7.DAL.Entities;
using Hl7.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using NHapi.Base;
using System.Text.Json;

namespace Hl7.App.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly ISuiEncoder _encoder;
    private readonly IAppointmentRepository _repo;
    private readonly IMapper _mapper;

    public AppointmentController(ISuiEncoder encoder, 
        IAppointmentRepository repo, IMapper mapper)
    {
        _encoder = encoder;
        _repo = repo;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SendAppointment(AppointmentDto appointment)
    {
        try
        {
            var entity = _mapper.Map<Appointment>(appointment); 
            var appointmentId = await _repo.AddAppointment(entity);
            var suiMessage = _encoder.Encode(appointment);

            var appointmentResult = new SendAppointmentResponse()
            {
                AppointmentId = appointmentId,
                SuiMessage = suiMessage
            }; 

            await _repo.AddSendAppointmentResponse(appointmentResult);

            return Ok(suiMessage);
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

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAppointments()
    {
        var results = await _repo.GetAll(); 
        return Ok(results);
    }
}
