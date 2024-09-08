using Hl7.App.Dto;
using NHapi.Base.Parser;
using NHapi.Base.Validation.Implementation;
using NHapi.Model.V271.Datatype;
using NHapi.Model.V271.Message;
using NHapi.Model.V271.Segment;

namespace Hl7.App.Services;

public class MdmDecoder : IMdmDecoder
{
    public MedicalRecordDto Decode(string message)
    {
        var parser = new PipeParser()
        {
            ValidationContext = new StrictValidation()
        };
        var parsedMessage = parser.Parse(message);
        var mdm = parsedMessage as MDM_T02;

        if (parsedMessage is null)
            return null;

        return BuildMedicalRecord(mdm);
    }

    private MedicalRecordDto BuildMedicalRecord(MDM_T02 mdm)
    {
        var patientName = mdm.PID.GetPatientName().FirstOrDefault();
        var patientAddress = mdm.PID.GetPatientAddress().FirstOrDefault();
        var dateOfBirth = mdm.PID.DateTimeOfBirth.GetAsDate();
        var phoneHome = mdm.PID.GetPhoneNumberHome().FirstOrDefault();
        var phoneBusiness = mdm.PID.GetPhoneNumberBusiness().FirstOrDefault();

        var alternateIdentifier = mdm.PID.AlternatePatientIDPID; 

        var patient = new PatientDtoMdm()
        {
            Id = alternateIdentifier.IDNumber.Value,
            DocumentType = alternateIdentifier.IdentifierTypeCode.Value,
            ParentSurname = patientName?.FamilyName.Surname.Value,
            MaternalSurname = mdm.PID.GetMotherSMaidenName()?.FirstOrDefault()?.FamilyName.Surname.Value,
            Name = patientName?.GivenName.Value,
            DateOfBirth = dateOfBirth.Year == 1 ? null : dateOfBirth,
            Sex = mdm.PID.AdministrativeSex.Identifier.Value,
        };

        var observation = mdm.OBSERVATIONs.FirstOrDefault(); 
        var obx = observation?.OBX;
        var obr = (OBR)(mdm.Message.GetStructure("OBR"));
        var orderNumber = obr?.PlacerOrderNumber.EntityIdentifier.Value;

        var observationIdentifier = obx?.ObservationIdentifier;
        var observationValue = obx?.GetObservationValue().FirstOrDefault();
        var data = (RP)observationValue?.Data;
        var text = data.Components.FirstOrDefault()?.ToString();

        int? orderNumberValue = null;
        if (!string.IsNullOrWhiteSpace(orderNumber) && int.TryParse(orderNumber, out var num)) 
        {
            orderNumberValue = num; 
        }

        var modality = obx?.GetInterpretationCodes()?.FirstOrDefault()?.Identifier.Value;

        var doctorDocumentNumber = obr?.PlacerField1.Value;
        var doctorName = obr?.PlacerField2.Value;
 
        var doctor = new DoctorDto()
        {
            DocumentNumber = doctorDocumentNumber,
            Name = doctorName
        };

        var reportDate = obr.ObservationDateTime.GetAsDate();

        var studyUid = obx.ObservationIdentifier.Identifier.Value; 

        var record = new MedicalRecordDto()
        {
            OrderNumber = orderNumberValue,
            StudyCode = observationIdentifier?.AlternateIdentifier.Value,
            StudyName = observationIdentifier?.Text.Value,
            ReportURL = obx.ObservationSubID.Value,
            ReportText = text,
            ServiceName = obx.ProducerSID.Identifier.Value,
            Modality = modality,
            Patient = patient,
            Doctor = doctor,
            ReportDate = reportDate.Year == 1 ? null : reportDate,
            StudyUID = studyUid
        };

        return record; 
    }
}
