using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hl7.DAL.Migrations;

/// <inheritdoc />
public partial class EventTimeAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "EventTime",
            table: "SendMedicalRecordRequest",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "EventTime",
            table: "Appointment",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "EventTime",
            table: "SendMedicalRecordRequest");

        migrationBuilder.DropColumn(
            name: "EventTime",
            table: "Appointment");
    }
}
