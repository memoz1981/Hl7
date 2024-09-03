namespace Hl7.App.Dto;

public class PatientDto
{
    public string Id { get; set; }
    public string DocumentType { get; set; }
    public string ParentSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    // M, F, O
    public string Sex { get; set; }
    public string Address { get; set; }
    public string Locality { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

