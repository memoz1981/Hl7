using AutoMapper;
using Hl7.App.Dto;
using Hl7.App.Services;
using Hl7.App.Utilities;
using Hl7.DAL.Entities;
using Hl7.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hl7.App.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly ISuiEncoder _encoder;
    private readonly IAppointmentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IFileLogger _logger;

    public AppointmentController(ISuiEncoder encoder,
        IAppointmentRepository repo, IMapper mapper, IFileLogger logger)
    {
        _encoder = encoder;
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SendAppointment(AppointmentDto appointment)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
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
        catch (Exception ex)
        {
            await _logger.Log(ex);
            return StatusCode(500, $"Some error occured: {ex.GetType().Name} - {ex.Message}");
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAppointments()
    {
        try
        {
            var results = await _repo.GetAll();
            return Ok(results);
        }
        catch (Exception ex)
        {
            await _logger.Log(ex);
            return StatusCode(500, $"Some error occured: {ex.GetType().Name} - {ex.Message}");
        }
    }
}
