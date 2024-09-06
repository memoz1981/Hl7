using System.ComponentModel.DataAnnotations;

namespace Hl7.DAL.Entities;

public class Doctor
{
    [Key]
    public int Id { get; set; }
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}
