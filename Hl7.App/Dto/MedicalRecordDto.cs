namespace Hl7.App.Dto;

public class MedicalRecordDto
{
    public PatientDtoMdm Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public int? OrderNumber { get; set; }
    public string ServiceName { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string ReportURL { get; set; }
    public string ReportText { get; set; }
    public string Modality { get; set; }
    public DateTime? ReportDate { get; set; }
    public string StudyUID { get; set; }
}
