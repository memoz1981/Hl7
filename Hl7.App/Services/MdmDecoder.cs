﻿using Hl7.App.Dto;
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
        var parsedMessage = parser.Parse(message);
        var mdm = parsedMessage as MDM_T02;

        if (parsedMessage is null)
            return null;

        return BuildMedicalRecord(mdm);
    }

    private MedicalRecord BuildMedicalRecord(MDM_T02 mdm)
    {
        var patientName = mdm.PID.GetPatientName().FirstOrDefault();
        var patientAddress = mdm.PID.GetPatientAddress().FirstOrDefault();
        var dateOfBirth = mdm.PID.DateTimeOfBirth.GetAsDate();
        var phoneHome = mdm.PID.GetPhoneNumberHome().FirstOrDefault();
        var phoneBusiness = mdm.PID.GetPhoneNumberBusiness().FirstOrDefault();

        var alternateIdentifier = mdm.PID.AlternatePatientIDPID; 
        var identifier = mdm.PID.GetPatientIdentifierList().FirstOrDefault(); 

        var patient = new PatientDto()
        {
            Id = alternateIdentifier.IDNumber.Value,
            DocumentType = alternateIdentifier.IdentifierTypeCode.Value,
            ParentSurname = patientName?.FamilyName.Surname.Value,
            MaternalSurname = mdm.PID.GetMotherSMaidenName()?.FirstOrDefault()?.FamilyName.Surname.Value,
            Name = patientName?.GivenName.Value,
            DateOfBirth = dateOfBirth,
            Sex = mdm.PID.AdministrativeSex.Identifier.Value,
        };

        var doc = mdm.PV1.GetAttendingDoctor().FirstOrDefault(); 

        var doctor = new DoctorDto()
        {
            DocumentNumber = doc?.PersonIdentifier.Value,
            Name = BuildDoctorName(doc)
        };

        var observation = mdm.OBSERVATIONs.FirstOrDefault(); 
        var obx = observation?.OBX;
        var obr = (OBR)(mdm.Message.GetStructure("OBR"));
        var orderNumber = obr?.PlacerOrderNumber.EntityIdentifier.Value;

        var observationIdentifier = obx?.ObservationIdentifier;
        var observationValue = obx?.GetObservationValue().FirstOrDefault();
        var data = (RP)observationValue?.Data;
        var file = data.ExtraComponents.GetComponent(0);

        var fileText = string.Empty; 
        if (file is not null)
        {
            fileText = file.Data.ToString(); 
        }
        var byteArray = string.IsNullOrWhiteSpace(fileText) ? null : Convert.FromBase64String(fileText);

        var text = data.Components.FirstOrDefault()?.ToString();
        var codes = obx?.GetInterpretationCodes().FirstOrDefault();

        var record = new MedicalRecord()
        {
            OrderNumber = orderNumber,
            StudyCode = observationIdentifier?.AlternateIdentifier.Value,
            StudyName = observationIdentifier?.Text.Value,
            ReportURL = obx.ObservationSubID.Value,
            ReportText = text,
            ServiceName = obx.ProducerSID.Identifier.Value,
            Patient = patient,
            Doctor = doctor
        };

        return record; 
    }

    public OBR GetOBRFromOBX(OBX obxSegment)
    {
        // Ensure the OBX segment is part of an ORU_R01 message
        if (obxSegment.Message is ORU_R01 oruMessage)
        {
            var obr = obxSegment.Message as ORU_R01;

            return (OBR)obr.GetStructure("OBR"); 
            // Loop through all OBR segments to find the one that matches the OBX segment
            
        }

        // Return null if no related OBR segment is found
        return null;
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
