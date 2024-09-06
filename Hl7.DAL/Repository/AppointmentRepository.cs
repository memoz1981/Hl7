using Hl7.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.DAL.Repository;
public class AppointmentRepository
{
    private readonly Hl7DbContext _context;

    public AppointmentRepository(Hl7DbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAppointment(Appointment appointment)
    {
        _context.Appointment.Add(appointment); 
        _
    }
}
