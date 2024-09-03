using Hl7.App.Dto;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V23.Group;
using NHapi.Model.V23.Message; 

namespace Hl7.App.Services;

public class SuiEncoder : ISuiEncoder
{
    public string Encode(AppointmentDto appointment)
    {
        var parser = new PipeParser();
        var message = new SIU_S12();

        AssignMsh(appointment, message);
        AssignSCH(appointment, message);
        AssignPID(appointment, message);
        AssignPV1(appointment, message);
        AssignRGS(appointment, message);
        AssignAIG(appointment, message);
        AssignAIL(appointment, message);
        AssignAIP(appointment, message);

        //additional fields/properties here...

        return parser.Encode(message);
    }

    private void AssignMsh(AppointmentDto appoint, SIU_S12 message)
    {
        message.MSH.MessageType.MessageType.Value = "SUI";
        message.MSH.MessageType.TriggerEvent.Value = "S12";
        message.MSH.FieldSeparator.Value = "|";
        message.MSH.SendingApplication.NamespaceID.Value = "";
        message.MSH.SendingFacility.NamespaceID.Value = "";
        message.MSH.ReceivingApplication.NamespaceID.Value = "";
        message.MSH.ReceivingFacility.NamespaceID.Value = "";
        message.MSH.EncodingCharacters.Value = @"^~\&";
        message.MSH.VersionID.Value = "2.3";
        message.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDate(DateTime.Now);
        message.MSH.MessageControlID.Value = "messageControlId";
        message.MSH.ProcessingID.ProcessingID.Value = "P";
    }

    private void AssignSCH(AppointmentDto appointment, SIU_S12 message)
    {
        message.SCH.AppointmentDuration.Value = appointment.Duration.ToString(); //HOLD
        message.SCH.AppointmentType.Text.Value = appointment.AppointmentType; //HOLD
        message.SCH.AppointmentDurationUnits.Text.Value = ""; //Check
        message.SCH.PlacerAppointmentID.UniversalID.Value = appointment.AppointmentId.ToString(); //HOLD
    }

    private void AssignPID(AppointmentDto appointment, SIU_S12 message)
    {
        var pid = message.AddPATIENT();
        message.SCH.

        var patient = new SIU_S12_PATIENT(IGroup );

        pid.PatientIdentifierList[0].ID.Value = appointment.Patient.Id;
        pid.PatientIdentifierList[0].IdentifierTypeCode.Value = appointment.Patient.DocumentType;
        pid.PatientName.FamilyName.Surname.Value = appointment.Patient.ParentSurname;
        pid.PatientName.GivenName.Value = appointment.Patient.Name;
        pid.PatientDateOfBirth.TimeOfAnEvent.Value = appointment.Patient.DateOfBirth.ToString("yyyyMMdd");
        pid.PatientSex.Value = appointment.Patient.Sex;
    }

    private void AssignPV1(AppointmentDto appointment, SIU_S12 message)
    {
        throw new NotImplementedException();
    }

    private void AssignRGS(AppointmentDto appointment, SIU_S12 message)
    {
        throw new NotImplementedException();
    }

    private void AssignAIG(AppointmentDto appointment, SIU_S12 message)
    {
        throw new NotImplementedException();
    }

    private void AssignAIL(AppointmentDto appointment, SIU_S12 message)
    {
        throw new NotImplementedException();
    }

    private void AssignAIP(AppointmentDto appointment, SIU_S12 message)
    {
        throw new NotImplementedException();
    }
}
