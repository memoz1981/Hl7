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

    private void AssignMsh(AppointmentDto appointment, SIU_S12 message)
    {
        message.MSH.FieldSeparator.Value = "|";
        message.MSH.EncodingCharacters.Value = @"^~\&";
        message.MSH.SendingApplication.NamespaceID.Value = appointment.EstablishmentCode;
        message.MSH.SendingFacility.NamespaceID.Value = appointment.EstablishmentName; 

        message.MSH.MessageType.MessageType.Value = "SUI";
        message.MSH.MessageType.TriggerEvent.Value = "S12";
        
        message.MSH.VersionID.Value = "2.3";
        message.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDate(DateTime.Now);
        message.MSH.CharacterSet.Value = "UNICODE UTF-8"; 
    }

    private void AssignSCH(AppointmentDto appointment, SIU_S12 message)
    {
        message.SCH.AppointmentDuration.Value = appointment.DurationInMinutes.ToString();
        message.SCH.AppointmentType.Text.Value = appointment.AppointmentType; 
        message.SCH.AppointmentDurationUnits.Text.Value = "minutes"; //Check
        message.SCH.PlacerAppointmentID.EntityIdentifier.Value = appointment.AppointmentId.ToString(); 
    }

    private void AssignPID(AppointmentDto appointment, SIU_S12 message)
    {
        var patient = message.AddPATIENT();

        //public DateTime DateOfBirth { get; set; }
        //public string Sex { get; set; }

        //Identifier
        patient.PID.PatientIDExternalID.ID.Value = appointment.Patient.Id;
        patient.PID.PatientIDExternalID.IdentifierTypeCode.Value = appointment.Patient.DocumentType;

        //Name
        var patientName = patient.PID.GetPatientName().FirstOrDefault();
        //if (!patientName.Any())
        //    patientName.Append(new NHapi.Model.V23.Datatype.XPN(,)); 
        patientName.GivenName.Value = appointment.Patient.Name;
        patientName.FamilyName.Value = appointment.Patient.ParentSurname;
        patient.PID.MotherSMaidenName.FamilyName.Value = appointment.Patient.MaternalSurname;

        //Details
        patient.PID.Sex.Value = appointment.Patient.Sex;
        patient.PID.DateOfBirth.TimeOfAnEvent.Set(appointment.Patient.DateOfBirth, "yyyy-MM-dd");

        //    public string Address { get; set; }
        //public string Locality { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Country { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }

        //Address
        var address = patient.PID.GetPatientAddress().FirstOrDefault();
        address.StreetAddress.Value = appointment.Patient.Address;
        address.OtherDesignation.Value = appointment.Patient.Locality; 
        address.City.Value = appointment.Patient.City;
        address.StateOrProvince.Value = appointment.Patient.State; 
        address.Country.Value = appointment.Patient.Country;

        //Contacts
        var contacts = patient.PID.GetPhoneNumberHome().FirstOrDefault();
        contacts.PhoneNumber.Value = appointment.Patient.Phone; 
        contacts.EmailAddress.Value = appointment.Patient.Email;

        //Pv1
        var pv1 = patient.PV1.GetAdmittingDoctor().FirstOrDefault();
        pv1.GivenName.Value = appointment.Doctor.Name;
        pv1.IDNumber.Value = appointment.Doctor.DocumentNumber; 
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
        
    }

    private void AssignAIL(AppointmentDto appointment, SIU_S12 message)
    {
        
    }

    private void AssignAIP(AppointmentDto appointment, SIU_S12 message)
    {
        
    }
}
