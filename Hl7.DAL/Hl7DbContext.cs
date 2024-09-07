using Hl7.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hl7.DAL;
public class Hl7DbContext : DbContext
{
    public Hl7DbContext(DbContextOptions<Hl7DbContext> options) : base(options) { }

    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<MedicalRecord> MedicalRecord { get; set; }
    public DbSet<SendAppointmentResponse> SendAppointmentResponse { get; set; }
    public DbSet<SendMedicalRecordRequest> SendMedicalRecordRequest { get; set; }
}
