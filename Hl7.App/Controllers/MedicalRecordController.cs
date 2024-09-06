using Hl7.App.Dto;
using Hl7.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hl7.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordController : ControllerBase
{
    private readonly IMdmDecoder _decoder;

    public MedicalRecordController(IMdmDecoder decoder)
    {
        _decoder = decoder;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SendMedicalRecord()
    {
        try
        {
            using var reader = new StreamReader(HttpContext.Request.Body);
            var mdmText = await reader.ReadToEndAsync();

            var result = _decoder.Decode(mdmText);

            //async database operations here... 
            await Task.CompletedTask;

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Some error occured, {ex.Message}");
        }
    }
}
