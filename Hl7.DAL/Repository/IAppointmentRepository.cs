using Hl7.DAL.Entities;

namespace Hl7.DAL.Repository;
public interface IAppointmentRepository
{
    public Task<int> AddAppointment(Appointment appointment);
    public Task AddSendAppointmentResponse(SendAppointmentResponse response);
    public Task<IEnumerable<SendAppointmentResponse>> GetAll(); 
}
