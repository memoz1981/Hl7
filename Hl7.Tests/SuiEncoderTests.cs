using Hl7.App.Dto;
using Hl7.App.Services;
using Shouldly;

namespace Hl7.Tests;

public class SuiEncoderTests
{
    private readonly SuiEncoder _encoder;

    public SuiEncoderTests()
    {
        _encoder = new(); 
    }

    [Fact]
    public void ShouldEncodeCorrectly()
    {
        //arrange
        var appointment = GetAppointment();

        //act
        var result = _encoder.Encode(appointment);

        //assert
        result.ShouldNotBeNull(); 
    }

    private AppointmentDto GetAppointment()
    {
        return new AppointmentDto
        {
            AppointmentId = 123,
            AppointmentDate = new DateTime(2024, 8, 1, 10, 25, 0),
            AppointmentType = "E",
            EstablishmentName = "HOSPITAL ABC",
            EstablishmentCode = "123456",
            ModalityId = "CR",
            DurationInMinutes = 0,
            Patient = new PatientDtoSui
            {
                Id = "0123456789",
                DocumentType = "DNI",
                ParentSurname = "Castillo",
                MaternalSurname = "Guzmán",
                Name = "Elizabeth",
                DateOfBirth = new DateTime(1980, 1, 2),
                Sex = "F",
                Address = "Calle 5 y Calle 2",
                Locality = "San Carlos",
                City = "Quito",
                State = "Ecuador",
                Country = "Pichincha",
                Phone = "9999999999",
                Email = "elizabeth@gmail.com"
            },
            Doctor = new DoctorDto
            {
                DocumentNumber = "0123456789",
                Name = "Carlos Perez"
            },
            OrderNumber = 123456,
            ServiceName = "TRAUMATOLOGIA",
            ThirdPartyName = null,
            EquipmentName = null,
            StudyCode = "TOM",
            StudyName = "TOMOGRAFIA",
            MedicalRegistration = "123456789",
            Aetitle = null,
            FileExtension = "pdf",
            AppointmentFile = ""
        };
    }
}
