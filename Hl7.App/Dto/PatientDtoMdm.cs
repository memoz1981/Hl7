namespace Hl7.App.Dto;

public class PatientDtoMdm
{
    public string Id { get; set; }
    public string DocumentType { get; set; }
    public string ParentSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Sex { get; set; }
}

