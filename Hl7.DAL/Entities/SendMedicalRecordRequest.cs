using System.ComponentModel.DataAnnotations;

namespace Hl7.DAL.Entities;
public class SendMedicalRecordRequest
{
    [Key]
    public int Id { get; set; }
    public DateTime EventTime { get; set; }
    public string MdmMessage { get; set; }
}
