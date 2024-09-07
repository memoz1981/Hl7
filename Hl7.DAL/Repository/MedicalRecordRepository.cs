using Hl7.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hl7.DAL.Repository;
public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly Hl7DbContext _context;

    public MedicalRecordRepository(Hl7DbContext context)
    {
        _context = context;
    }

    public async Task AddMedicalRecord(MedicalRecord record)
    {
        await _context.MedicalRecord.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddSendMedicalRecordRequest(SendMedicalRecordRequest request)
    {
        request.EventTime = DateTime.UtcNow; 
        await _context.SendMedicalRecordRequest.AddAsync(request);
        await _context.SaveChangesAsync();

        return request.Id; 
    }

    public async Task<IEnumerable<SendMedicalRecordRequest>> GetAll()
    {
        return await _context.SendMedicalRecordRequest
            .Include(req => req.MedicalRecord)
            .ThenInclude(rec => rec.Patient)
            .Include(req => req.MedicalRecord)
            .ThenInclude(rec => rec.Doctor)
            .ToListAsync(); 
    }
}
