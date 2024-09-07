using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hl7.DAL.Entities;

public class Doctor
{
    [Key]
    public int Id { get; set; }
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public int? AppointmentId { get; set; }
    [JsonIgnore]
    public Appointment Appointment { get; set; }
    public int? MedicalRecordId { get; set; }
    [JsonIgnore]
    public MedicalRecord MedicalRecord { get; set; }
}
