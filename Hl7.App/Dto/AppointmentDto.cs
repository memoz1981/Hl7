namespace Hl7.App.Dto;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentType { get; set; }
    public string EstablishmentName { get; set; }
    public string EstablishmentCode { get; set; }
    public string ModalityId { get; set; }
    public int DurationInMinutes { get; set; }
    public PatientDtoSui Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public string OrderNumber { get; set; }
    public string ServiceName { get; set; }
    public string ThirdPartyName { get; set; }
    public string EquipmentName { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string MedicalRegistration { get; set; }
    public string Aetitle { get; set; }
    public string FileExtension { get; set; }
    public string AppointmentFile { get; set; }
}
