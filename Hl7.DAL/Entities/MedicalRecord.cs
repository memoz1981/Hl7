using System.ComponentModel.DataAnnotations;

namespace Hl7.DAL.Entities;
public class MedicalRecord
{
    [Key]
    public int Id { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public string OrderNumber { get; set; }
    public string ServiceName { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string ReportURL { get; set; }
    public string ReportText { get; set; }
    public int SendMedicalRecordRequestId { get; set; }
    public SendMedicalRecordRequest SendMedicalRecordRequest { get; set; }
}
