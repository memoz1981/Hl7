using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hl7.DAL.Entities;
public class SendAppointmentResponse
{
    [Key]
    public int Id { get; set; }
    public string SuiMessage { get; set; }
    public int AppointmentId { get; set; }
    [JsonIgnore]
    public Appointment Appointment { get; set; }
}
