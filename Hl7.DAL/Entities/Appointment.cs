using System.ComponentModel.DataAnnotations;

namespace Hl7.DAL.Entities;

public class Appointment
{
    [Key]
    public int Id { get; set; }
    public DateTime EventTime { get; set; }
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentType { get; set; }
    public string EstablishmentName { get; set; }
    public string EstablishmentCode { get; set; }
    [MaxLength(2)]
    public string ModalityId { get; set; }
    public int DurationInMinutes { get; set; }
    public int OrderNumber { get; set; }
    [MaxLength(100)]
    public string ServiceName { get; set; }
    public string ThirdPartyName { get; set; }
    public string EquipmentName { get; set; }
    public string StudyCode { get; set; }
    public string StudyName { get; set; }
    public string MedicalRegistration { get; set; }
    public string Aetitle { get; set; }
    [MaxLength(3)] // jpg, png , pdf
    public string FileExtension { get; set; }
    public string AppointmentFile { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}