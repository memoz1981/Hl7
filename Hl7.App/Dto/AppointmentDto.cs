namespace Hl7.App.Dto;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentType { get; set; }
    public string ModalityId { get; set; }
    public int Duration { get; set; }
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public string ServiceName { get; set; }
    public string ThirdPartyName { get; set; }
    public string EquipmentName { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string MedicalRegistration { get; set; }
    public string Aetitle { get; set; }
    // jpg, png , pdf
    public string FileExtension { get; set; }
    public byte[] AppointmentFile { get; set; }
}
