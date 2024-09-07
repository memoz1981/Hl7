using Hl7.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hl7.DAL.Repository;
public class AppointmentRepository :IAppointmentRepository
{
    private readonly Hl7DbContext _context;

    public AppointmentRepository(Hl7DbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAppointment(Appointment appointment)
    {
        appointment.EventTime = DateTime.UtcNow;
        await _context.Appointment.AddAsync(appointment); 
        await _context.SaveChangesAsync();

        return appointment.Id; 
    }

    public async Task AddSendAppointmentResponse(SendAppointmentResponse response)
    {
        await _context.SendAppointmentResponse.AddAsync(response);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        return await _context.Appointment
            .Include(app => app.SendAppointmentResponse)
            .Include(app => app.Patient)
            .Include(app => app.Doctor)
            .ToListAsync();
    }
}
