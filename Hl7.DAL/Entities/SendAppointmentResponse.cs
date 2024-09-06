using System.ComponentModel.DataAnnotations;

namespace Hl7.DAL.Entities;
public class SendAppointmentResponse
{
    [Key]
    public int Id { get; set; }
    public string SuiMessage { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}
