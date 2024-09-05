namespace Hl7.App.Dto;

public class MedicalRecord
{
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public string OrderNumber { get; set; }
    public string ServiceName { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string ReportURL { get; set; }
    public string ReportText { get; set; }
}
