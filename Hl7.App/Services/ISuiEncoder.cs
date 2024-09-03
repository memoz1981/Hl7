using Hl7.App.Dto;

namespace Hl7.App.Services;

public interface ISuiEncoder
{
    public string Encode(AppointmentDto appointment);
}
