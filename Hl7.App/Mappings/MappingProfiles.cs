using AutoMapper;
using Hl7.App.Dto;
using Hl7.DAL.Entities;

namespace Hl7.App.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<Patient, PatientDtoMdm>().ReverseMap();
        CreateMap<Patient, PatientDtoSui>().ReverseMap();
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap(); 
    }
}
