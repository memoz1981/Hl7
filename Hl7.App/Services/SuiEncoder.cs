using Hl7.App.Dto;
using NHapi.Base.Parser;
using NHapi.Model.V251.Message;

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
        AddAis(message, appointment);
        AddNTE(message, appointment);
        return parser.Encode(message);
    }

    private void AssignMsh(AppointmentDto appointment, SIU_S12 message)
    {
        message.MSH.FieldSeparator.Value = "|";
        message.MSH.EncodingCharacters.Value = @"^~\&";
        message.MSH.SendingApplication.NamespaceID.Value = appointment.EstablishmentCode;
        message.MSH.SendingFacility.NamespaceID.Value = appointment.EstablishmentName; 

        message.MSH.MessageType.MessageCode.Value = "SUI";
        message.MSH.MessageType.TriggerEvent.Value = "S12";
        
        message.MSH.VersionID.VersionID.Value = "2.5.1";
        message.MSH.DateTimeOfMessage.Time.SetLongDate(DateTime.UtcNow);
        message.MSH.GetCharacterSet(0).Value = "UNICODE UTF-8"; 
    }

    private void AssignSCH(AppointmentDto appointment, SIU_S12 message)
    {
        message.SCH.GetAppointmentTimingQuantity(0).StartDateTime.Time.Value 
            = appointment.AppointmentDate.ToString("yyyyMMdd");
        message.SCH.AppointmentReason.Identifier.Value = appointment.ModalityId; 
        message.SCH.AppointmentDuration.Value = appointment.DurationInMinutes.ToString();
        message.SCH.AppointmentDurationUnits.Text.Value = "MIN";
        message.SCH.PlacerAppointmentID.EntityIdentifier.Value = appointment.OrderNumber.ToString(); 
    }

    private void AssignPID(AppointmentDto appointment, SIU_S12 message)
    {
        var patient = message.AddPATIENT();

        var id = patient.PID.GetAlternatePatientIDPID(0); 

        //Identifier
        id.IDNumber.Value = appointment.Patient.Id;
        id.IdentifierTypeCode.Value = appointment.Patient.DocumentType;

        //Name
        var patientName = patient.PID.GetPatientName(0);
        patientName.GivenName.Value = appointment.Patient.Name;
        patientName.FamilyName.Surname.Value = appointment.Patient.ParentSurname;
        patient.PID.GetMotherSMaidenName(0).FamilyName.Surname.Value 
            = appointment.Patient.MaternalSurname;

        //Details
        patient.PID.AdministrativeSex.Value = appointment.Patient.Sex;
        if(appointment.Patient.DateOfBirth.HasValue)
            patient.PID.DateTimeOfBirth.Time.Set(appointment.Patient.DateOfBirth.Value, "yyyyMMdd");

        //Address
        var address = patient.PID.GetPatientAddress(0);
        address.StreetAddress.StreetOrMailingAddress.Value = appointment.Patient.Address;
        address.OtherDesignation.Value = appointment.Patient.Locality; 
        address.City.Value = appointment.Patient.City;
        address.StateOrProvince.Value = appointment.Patient.State; 
        address.Country.Value = appointment.Patient.Country;

        //Contacts
        var contacts = patient.PID.GetPhoneNumberHome(0);
        contacts.TelephoneNumber.Value = appointment.Patient.Phone; 
        contacts.EmailAddress.Value = appointment.Patient.Email;

        //Pv1
        var doctor = patient.PV1.GetAttendingDoctor(0);
        patient.PV1.SetIDPV1.Value = "1"; 
        doctor.FamilyName.Surname.Value = appointment.Doctor.Name;
        doctor.IDNumber.Value = appointment.Doctor.DocumentNumber;
        patient.PV1.AdmissionType.Value = appointment.AppointmentType;
        patient.PV1.HospitalService.Value = appointment.ServiceName;
        patient.PV1.GetFinancialClass(0).FinancialClassCode.Value = appointment.ThirdPartyName;
        patient.PV1.GetOtherHealthcareProvider(0).IDNumber.Value = appointment.EquipmentName; 

        var obx = patient.AddOBX();
        obx.SetIDOBX.Value = "1";
    }

    private void AddNTE(SIU_S12 message, AppointmentDto appointment)
    {
        var nte = message.AddNTE(); 
        nte.SetIDNTE.Value = "1";
        nte.SourceOfComment.Value = appointment.FileExtension;
        nte.GetComment(0).Value = appointment.AppointmentFile; 
    }

    private void AddAis(SIU_S12 message, AppointmentDto appointment)
    {
        var resource = message.AddRESOURCES();
        var ais = resource.AddSERVICE();
        ais.AIS.SetIDAIS.Value = "1";
        ais.AIS.UniversalServiceIdentifier.Identifier.Value = appointment.StudyCode; 
        ais.AIS.UniversalServiceIdentifier.Text.Value = appointment.StudyName;

        var ail = resource.AddLOCATION_RESOURCE();
        var locationResourceId = ail.AIL.GetLocationResourceID(0);
        locationResourceId.PointOfCare.Value = appointment.MedicalRegistration;
        locationResourceId.Room.Value = appointment.Aetitle; 

    }
}
