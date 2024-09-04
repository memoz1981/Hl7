using Hl7.App.Dto;
using NHapi.Base.Parser;

namespace Hl7.App.Services;
using NHapi.Model.V23.Message; 

public class MdmDecoder : IMdmDecoder
{
    public MedicalRecord Decode(string message)
    {
        var parser = new PipeParser();
        var enc = parser.GetEncoding(message); 

        var mdm = (MDM_T02)parser.Parse(message);

        var record = new MedicalRecord();

        //extract other segments and build the object

        return record;
    }
}
