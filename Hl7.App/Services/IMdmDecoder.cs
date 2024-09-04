using Hl7.App.Dto;

namespace Hl7.App.Services;

public interface IMdmDecoder
{
    public MedicalRecord Decode(string message); 
}
