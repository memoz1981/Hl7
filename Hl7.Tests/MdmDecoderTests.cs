using Hl7.App.Services;
using Shouldly;

namespace Hl7.Tests;

public class MdmDecoderTests
{
    private readonly MdmDecoder _decoder;

    public MdmDecoderTests()
    {
        _decoder = new();
    }

    [Fact]
    public void ShouldExtractData()
    {
        //arrange
        var message = @"MSH|^~\&|MESA_OP|XYZ_HOSPITAL|iFW|ABC_HOSPITAL|20090130123||MDM^T02"+
"|20090130123|P| 2.3 |||NE|EVN |T02|200901301235|"+
"PID |||||DOE^JOHN||19920115|M||||||||||G83186|005729288| PV1"+
"||1|CE||||12345^WILLIS^LAYLA^H|||||||||||"+
"TXA |1|HP^History & Physical|TX|200901151010|||||||||||||DO||AV|"+
"OBX |1|TX|||"+
"OBX|2|TX|||"+ 
"OBX|3|TX|||"+
"OBX|4|TX||| Name : TEST,TEST"+
"OBX|5|TX||| MR #: 123006"+ 
"HOSPITAL TEST||||||P|||||| Morrisville , Vermont 05661||||||P||||||"+
"OBX|6|TX||| Physician:JAMES CLEA DOB :"+
"Date:01/13/09||||||P||||||"+
"Sex: M Age: 19"+
"HISTORY & PHYSICAL ||||||P||||||"+
"Room #: 039A||||||P||||||"+
"Acct . #: G8318||||||P 07/13/1989 Admit "+
"OBX|7|TX||| Physician : KAEDING J Stay Type : I/PD/C||||||P||||||"+
"OBX|8|TX||| Physician : KEITH P||||||P||||||"+ 
"OBX|9|TX|||CC: Dizziness and weakness with elevated sugars .| |||||P|||"+
"OBX|10|TX||| HPI : This is a 19-year-old man with multiple admissions for poorly"+
"controlled ||||||P||||||";


        //act
        var result = _decoder.Decode(message);

        //assert
        result.ShouldNotBeNull();
    }
}