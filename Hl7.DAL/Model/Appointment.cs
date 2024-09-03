using System.ComponentModel.DataAnnotations;

namespace Hl7.DAL.Model;

public class Appointment
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentType { get; set; }
    [MaxLength(2)]
    public string ModalityId { get; set; }
    public int Duration { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
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
    public byte[] AppointmentFile { get; set; }
}
public class Patient
{
    [MaxLength(20)]
    public string Id { get; set; }
    [MaxLength(20)]
    public string DocumentType { get; set; }
    [MaxLength(32)]
    public string ParentSurname { get; set; }
    [MaxLength(32)]
    public string MaternalSurname { get; set; }
    [MaxLength(32)]
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    [MaxLength(1)] // M, F, O
    public string Sex { get; set; }
    public string Address { get; set; }
    public string Locality { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
public class Doctor
{
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
}
