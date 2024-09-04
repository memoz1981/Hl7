namespace Hl7.App.Dto;

public class MedicalRecord
{
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public string AccessionNumber { get; set; }
    public string StudyInstanceUID { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string ReportURL { get; set; }
    public string ReportText { get; set; }
    public byte[] ReportFile { get; set; }
    public string ModalityId { get; set; }
    public string ServiceName { get; set; }
}
