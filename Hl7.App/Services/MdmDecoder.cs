using Hl7.App.Dto;
using NHapi.Base.Parser;
using NHapi.Model.V271.Datatype;
using NHapi.Model.V271.Message;
using NHapi.Model.V271.Segment;
using System.Text;

namespace Hl7.App.Services;

public class MdmDecoder : IMdmDecoder
{
    public MedicalRecord Decode(string message)
    {
        var parser = new PipeParser();
        var mdm = (MDM_T02)parser.Parse(message);

        return BuildMedicalRecord(mdm);
    }

    private MedicalRecord BuildMedicalRecord(MDM_T02 mdm)
    {
        var patientName = mdm.PID.GetPatientName().FirstOrDefault();
        var patientAddress = mdm.PID.GetPatientAddress().FirstOrDefault();
        var dateOfBirth = mdm.PID.DateTimeOfBirth.GetAsDate();
        var phoneHome = mdm.PID.GetPhoneNumberHome().FirstOrDefault();
        var phoneBusiness = mdm.PID.GetPhoneNumberBusiness().FirstOrDefault();

        var patient = new PatientDto()
        {
            Id = mdm.PID.PatientID.IDNumber.Value,
            DocumentType = mdm.PID.PatientID.TypeName,
            ParentSurname = patientName?.FamilyName.Surname.Value,
            MaternalSurname = patientName?.FamilyName.OwnSurname.Value,
            Name = patientName?.GivenName.Value,
            DateOfBirth = dateOfBirth,
            Sex = mdm.PID.AdministrativeSex.Identifier.Value,
            Address = patientAddress?.StreetAddress.StreetOrMailingAddress.Value,
            Locality = patientAddress?.ZipOrPostalCode.Value,
            City = patientAddress?.City.Value,
            State = patientAddress?.StateOrProvince.Value,
            Country = patientAddress?.Country.Value,
            Phone = (phoneHome??phoneBusiness)?.TelephoneNumber.Value,
            Email = ""
        };

        var doc = mdm.PV1.GetAttendingDoctor().FirstOrDefault(); 

        var doctor = new DoctorDto()
        {
            DocumentNumber = doc?.PersonIdentifier.Value,
            Name = BuildDoctorName(doc)
        };

        var observation = mdm.OBSERVATIONs.FirstOrDefault(); 
        var obx = observation.OBX;
        var obr = (OBR)(mdm.Message.GetAll("OBR").FirstOrDefault());
        var orderNumber = obr?.GetOrderCallbackPhoneNumber()?.FirstOrDefault()?.TelephoneNumber.Value;

        var observationIdentifier = obx?.ObservationIdentifier;
        var observationValue = obx?.GetObservationValue().FirstOrDefault();
        var data = (RP)observationValue?.Data;
        var text = data.Components.FirstOrDefault()?.ToString();

        var codes = obx?.GetInterpretationCodes().FirstOrDefault();

        var record = new MedicalRecord()
        {
            AccessionNumber = obr?.GetOrderCallbackPhoneNumber()?.FirstOrDefault()?.TelephoneNumber.Value,
            StudyInstanceUID = observationIdentifier?.Identifier.Value,
            StudyCode = observationIdentifier?.AlternateIdentifier.Value,
            StudyName = observationIdentifier?.Text.Value,
            ReportURL = obx.ObservationSubID.Value,
            ReportText = text,
            ReportFile = new byte[1],
            ModalityId = codes?.Identifier.Value,
            ServiceName = obx.ProducerSID.Identifier.Value,
            Patient = patient,
            Doctor = doctor
        };

        return record; 
    }

    private string BuildDoctorName(XCN doc)
    {
        if (doc == null)
            return string.Empty; 

        var builder = new StringBuilder();
        var a = builder.ToString();

        if (!string.IsNullOrWhiteSpace(doc.DegreeEgMD.Value))
        {
            builder.Append(doc.DegreeEgMD.Value);
            builder.Append(" "); 
        }

        if (!string.IsNullOrWhiteSpace(doc.PrefixEgDR.Value))
        {
            builder.Append(doc.PrefixEgDR.Value);
            builder.Append(" ");
        }

        if (!string.IsNullOrWhiteSpace(doc.GivenName.Value))
        {
            builder.Append(doc.GivenName.Value);
            builder.Append(" ");
        }

        if (!string.IsNullOrWhiteSpace(doc.FamilyName.Surname.Value))
        {
            builder.Append(doc.FamilyName.Surname.Value);
        }

        return builder.ToString();
    }
}
