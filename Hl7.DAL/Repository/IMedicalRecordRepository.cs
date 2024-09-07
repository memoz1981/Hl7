using Hl7.DAL.Entities;

namespace Hl7.DAL.Repository;
public interface IMedicalRecordRepository
{
    public Task<int> AddSendMedicalRecordRequest(SendMedicalRecordRequest request);
    public Task AddMedicalRecord(MedicalRecord record);
    public Task<IEnumerable<SendMedicalRecordRequest>> GetAll(); 
}
