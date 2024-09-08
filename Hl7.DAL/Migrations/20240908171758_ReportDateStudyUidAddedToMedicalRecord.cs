using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hl7.DAL.Migrations; 

/// <inheritdoc />
public partial class ReportDateStudyUidAddedToMedicalRecord : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "ReportDate",
            table: "MedicalRecord",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "StudyUID",
            table: "MedicalRecord",
            type: "TEXT",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ReportDate",
            table: "MedicalRecord");

        migrationBuilder.DropColumn(
            name: "StudyUID",
            table: "MedicalRecord");
    }
}
