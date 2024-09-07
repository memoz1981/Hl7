using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hl7.DAL.Migrations; 

/// <inheritdoc />
public partial class ModalityColumn : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Modality",
            table: "MedicalRecord",
            type: "TEXT",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Modality",
            table: "MedicalRecord");
    }
}
