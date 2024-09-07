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
    public void ShouldDecodeMdmMessageWithFileCorrectly()
    {
        var message = @$"MSH|^~\&|RIS_VM|NOVACLINICA|VMRIS|NOVACLINICA|Fecha/Hora Mensage||MDM^T02|ID_UNICO_MSG|P|2.7.1|||||||UNICODE UTF-8
PID|||24125412^^^^DNI|NRO DOC^^^^DOC TYPE|APELLIDO_PACIENTE33^NOMBRES33|APELLIDO MATERNO33|19780723|M|||CALLE 54^CAPITAL^CIUDAD^MENDOZA^^ARGENTINA||456456456^MAIL@MAIL.COM^|
OBR|1|222^1|FILLER ORDER NUMBER||||FECHA HORA ESTUDIO||||||||||ACCESSIONNUMBER|CodigoMedico|informante|
OBX|1|RP|studyintanceUID^NOMBRE ESTUDIO^^CODIGO ESTUDIO|URL| TXT^^^^{TestData.Base64String}|||MODALIDAD|||||||SERVICIO"; 
        //act
        var result = _decoder.Decode(message);

        //assert
        result.ShouldNotBeNull();
        result.OrderNumber.ShouldBe(222);
        result.StudyCode.ShouldBe("CODIGO ESTUDIO");
        result.StudyName.ShouldBe("NOMBRE ESTUDIO");
        result.ReportURL.ShouldBe("URL");
        result.ReportText.ShouldBe("TXT");
        result.ServiceName.ShouldBe("SERVICIO");

        //PID|1| |||APELLIDO PAC^Nombre Pac^^^^^^||20100201|||
        var patient = result.Patient;
        patient.DateOfBirth.ShouldBe(new DateTime(1978, 07, 23));
        patient.DocumentType.ShouldBe("DOC TYPE");
        patient.Id.ShouldBe("NRO DOC");
        patient.MaternalSurname.ShouldBe("APELLIDO MATERNO33");
        patient.Name.ShouldBe("NOMBRES33");
        patient.ParentSurname.ShouldBe("APELLIDO_PACIENTE33");
        patient.Sex.ShouldBe("M");

        var doctor = result.Doctor;
        doctor.DocumentNumber.ShouldBeNullOrEmpty();
        doctor.Name.ShouldBeNullOrEmpty(); 
    }
}