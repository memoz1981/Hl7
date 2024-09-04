using Hl7.App.Dto;
using NHapi.Base.Parser;
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

        pid.PID.SetIDPatientID.Value = appointment.Patient.Id;
        //document type
        pid.PID.Sex.Value = appointment.Patient.Sex;
        pid.PID.DateOfBirth.TimeOfAnEvent.Set(appointment.Patient.DateOfBirth, "yyyy-MM-dd");
        pid.PID.MotherSMaidenName.FamilyName.Value = appointment.Patient.MaternalSurname; 

    }

    private void AssignPV1(AppointmentDto appointment, SIU_S12 message)
    {
        
    }

    private void AssignRGS(AppointmentDto appointment, SIU_S12 message)
    {
        var res = message.AddRESOURCES(); 
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
