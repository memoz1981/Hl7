using Hl7.App.Dto;
using NHapi.Base.Parser;
using NHapi.Model.V23.Message;
using NHapi.Model.V23.Segment;

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
        AddNTE(message, appointment); 
        AddAis(message, appointment);

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
        message.SCH.AppointmentDurationUnits.Text.Value = "MIN"; //Check
        message.SCH.PlacerAppointmentID.EntityIdentifier.Value = appointment.AppointmentId.ToString(); 
    }

    private void AssignPID(AppointmentDto appointment, SIU_S12 message)
    {
        var patient = message.AddPATIENT();

        var id = patient.PID.GetAlternatePatientID(0); 

        //Identifier
        id.ID.Value = appointment.Patient.Id;
        id.IdentifierTypeCode.Value = appointment.Patient.DocumentType;

        //Name
        var patientName = patient.PID.GetPatientName(0);
        patientName.GivenName.Value = appointment.Patient.Name;
        patientName.FamilyName.Value = appointment.Patient.ParentSurname;
        patient.PID.MotherSMaidenName.FamilyName.Value = appointment.Patient.MaternalSurname;

        //Details
        patient.PID.Sex.Value = appointment.Patient.Sex;
        patient.PID.DateOfBirth.TimeOfAnEvent.Set(appointment.Patient.DateOfBirth, "yyyy-MM-dd");

        //Address
        var address = patient.PID.GetPatientAddress(0);
        address.StreetAddress.Value = appointment.Patient.Address;
        address.OtherDesignation.Value = appointment.Patient.Locality; 
        address.City.Value = appointment.Patient.City;
        address.StateOrProvince.Value = appointment.Patient.State; 
        address.Country.Value = appointment.Patient.Country;

        //Contacts
        var contacts = patient.PID.GetPhoneNumberHome(0);
        contacts.PhoneNumber.Value = appointment.Patient.Phone; 
        contacts.EmailAddress.Value = appointment.Patient.Email;

        //Pv1
        var doctor = patient.PV1.GetAdmittingDoctor(0);
        doctor.GivenName.Value = appointment.Doctor.Name;
        doctor.IDNumber.Value = appointment.Doctor.DocumentNumber;

        var obx = patient.AddOBX();
        AddObx(obx, appointment);
    }

    private void AddObx(OBX obx, AppointmentDto appointment)
    {
        obx.SetIDOBX.Value = "1"; 
        obx.ObservationIdentifier.AlternateIdentifier.Value = appointment.StudyCode;
        obx.ObservationIdentifier.Text.Value = appointment.StudyName;
        obx.ProducerSID.Identifier.Value = appointment.ServiceName;
        var observationValue = obx.GetObservationValue(0);
        var comp1 = observationValue.ExtraComponents.GetComponent(0);  
    }

    private void AddNTE(SIU_S12 message, AppointmentDto appointment)
    {
        var nte = message.AddNTE(); 
        nte.SetIDNotesAndComments.Value = "1";
        nte.SourceOfComment.Value = appointment.FileExtension;
        nte.GetComment(0).Value = appointment.AppointmentFile; 
    }

    private void AddAis(SIU_S12 message, AppointmentDto appointment)
    {
        var resource = message.AddRESOURCES();
        var ais = resource.AddSERVICE();
        ais.AIS.UniversalServiceIdentifier.Text.Value = appointment.Aetitle;
        ais.AIS.SetIDAIS.Value = "1"; 
    }

    /*
        public string ModalityId { get; set; }
        public string ThirdPartyName { get; set; }
        public string EquipmentName { get; set; }
        public string MedicalRegistration { get; set; }
        
        public string OrderNumber { get; set; }
     */
}
