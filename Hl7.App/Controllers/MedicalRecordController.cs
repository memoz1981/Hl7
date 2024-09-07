using AutoMapper;
using Hl7.App.Services;
using Hl7.App.Utilities;
using Hl7.DAL.Entities;
using Hl7.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hl7.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordController : ControllerBase
{
    private readonly IMdmDecoder _decoder;
    private readonly IMedicalRecordRepository _repo;
    private readonly IMapper _mapper;
    private readonly IFileLogger _logger;

    public MedicalRecordController(IMdmDecoder decoder, IMedicalRecordRepository repo, IMapper mapper, IFileLogger logger)
    {
        _decoder = decoder;
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SendMedicalRecord()
    {
        try
        {
            using var reader = new StreamReader(HttpContext.Request.Body);
            var mdmText = await reader.ReadToEndAsync();

            var sendMedicalRecordRequest = new SendMedicalRecordRequest()
            {
                MdmMessage = mdmText,
            };

            var sendMedicalRequestId 
                = await _repo.AddSendMedicalRecordRequest(sendMedicalRecordRequest);

            var medicalRecordDto = _decoder.Decode(mdmText);

            var medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDto);
            medicalRecord.SendMedicalRecordRequestId = sendMedicalRequestId;

            await _repo.AddMedicalRecord(medicalRecord); 

            return Ok(medicalRecordDto);
        }
        catch (Exception ex)
        {
            await _logger.Log(ex);
            return StatusCode(500, $"Some error occured: {ex.GetType().Name} - {ex.Message}");
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetMedicalRecords()
    {
        try
        {
            var result = await _repo.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            await _logger.Log(ex);
            return StatusCode(500, $"Some error occured: {ex.GetType().Name} - {ex.Message}");
        }
    }
}
