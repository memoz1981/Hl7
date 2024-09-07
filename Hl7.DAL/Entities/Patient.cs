using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hl7.DAL.Entities;

public class Patient
{
    [Key]
    public int PatientId { get; set; }
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
    public DateTime? DateOfBirth { get; set; }
    [MaxLength(1)] // M, F, O
    public string Sex { get; set; }
    public string Address { get; set; }
    public string Locality { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int AppointmentId { get; set; }
    [JsonIgnore]
    public Appointment Appointment { get; set; }
}
