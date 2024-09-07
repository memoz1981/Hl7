using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hl7.DAL.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Appointment",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                AppointmentId = table.Column<int>(type: "INTEGER", nullable: false),
                AppointmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                AppointmentType = table.Column<string>(type: "TEXT", nullable: true),
                EstablishmentName = table.Column<string>(type: "TEXT", nullable: true),
                EstablishmentCode = table.Column<string>(type: "TEXT", nullable: true),
                ModalityId = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                DurationInMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                OrderNumber = table.Column<int>(type: "INTEGER", nullable: false),
                ServiceName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                ThirdPartyName = table.Column<string>(type: "TEXT", nullable: true),
                EquipmentName = table.Column<string>(type: "TEXT", nullable: true),
                StudyCode = table.Column<string>(type: "TEXT", nullable: true),
                StudyName = table.Column<string>(type: "TEXT", nullable: true),
                MedicalRegistration = table.Column<string>(type: "TEXT", nullable: true),
                Aetitle = table.Column<string>(type: "TEXT", nullable: true),
                FileExtension = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                AppointmentFile = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointment", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SendMedicalRecordRequest",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                MdmMessage = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SendMedicalRecordRequest", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Doctor",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                DocumentNumber = table.Column<string>(type: "TEXT", nullable: true),
                Name = table.Column<string>(type: "TEXT", nullable: true),
                AppointmentId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Doctor", x => x.Id);
                table.ForeignKey(
                    name: "FK_Doctor_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Patient",
            columns: table => new
            {
                PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Id = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                DocumentType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                ParentSurname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                MaternalSurname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                Sex = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                Address = table.Column<string>(type: "TEXT", nullable: true),
                Locality = table.Column<string>(type: "TEXT", nullable: true),
                City = table.Column<string>(type: "TEXT", nullable: true),
                State = table.Column<string>(type: "TEXT", nullable: true),
                Country = table.Column<string>(type: "TEXT", nullable: true),
                Phone = table.Column<string>(type: "TEXT", nullable: true),
                Email = table.Column<string>(type: "TEXT", nullable: true),
                AppointmentId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Patient", x => x.PatientId);
                table.ForeignKey(
                    name: "FK_Patient_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SendAppointmentResponse",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                SuiMessage = table.Column<string>(type: "TEXT", nullable: true),
                AppointmentId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SendAppointmentResponse", x => x.Id);
                table.ForeignKey(
                    name: "FK_SendAppointmentResponse_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MedicalRecord",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                PatientId = table.Column<int>(type: "INTEGER", nullable: true),
                DoctorId = table.Column<int>(type: "INTEGER", nullable: true),
                OrderNumber = table.Column<string>(type: "TEXT", nullable: true),
                ServiceName = table.Column<string>(type: "TEXT", nullable: true),
                StudyCode = table.Column<string>(type: "TEXT", nullable: true),
                StudyName = table.Column<string>(type: "TEXT", nullable: true),
                ReportURL = table.Column<string>(type: "TEXT", nullable: true),
                ReportText = table.Column<string>(type: "TEXT", nullable: true),
                SendMedicalRecordRequestId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MedicalRecord", x => x.Id);
                table.ForeignKey(
                    name: "FK_MedicalRecord_Doctor_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctor",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_MedicalRecord_Patient_PatientId",
                    column: x => x.PatientId,
                    principalTable: "Patient",
                    principalColumn: "PatientId");
                table.ForeignKey(
                    name: "FK_MedicalRecord_SendMedicalRecordRequest_SendMedicalRecordRequestId",
                    column: x => x.SendMedicalRecordRequestId,
                    principalTable: "SendMedicalRecordRequest",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_AppointmentId",
            table: "Doctor",
            column: "AppointmentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MedicalRecord_DoctorId",
            table: "MedicalRecord",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_MedicalRecord_PatientId",
            table: "MedicalRecord",
            column: "PatientId");

        migrationBuilder.CreateIndex(
            name: "IX_MedicalRecord_SendMedicalRecordRequestId",
            table: "MedicalRecord",
            column: "SendMedicalRecordRequestId");

        migrationBuilder.CreateIndex(
            name: "IX_Patient_AppointmentId",
            table: "Patient",
            column: "AppointmentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_SendAppointmentResponse_AppointmentId",
            table: "SendAppointmentResponse",
            column: "AppointmentId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MedicalRecord");

        migrationBuilder.DropTable(
            name: "SendAppointmentResponse");

        migrationBuilder.DropTable(
            name: "Doctor");

        migrationBuilder.DropTable(
            name: "Patient");

        migrationBuilder.DropTable(
            name: "SendMedicalRecordRequest");

        migrationBuilder.DropTable(
            name: "Appointment");
    }
}
