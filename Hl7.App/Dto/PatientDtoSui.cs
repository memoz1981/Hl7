namespace Hl7.App.Dto;

public class PatientDtoSui : PatientDtoMdm
{
    public string Address { get; set; }
    public string Locality { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
