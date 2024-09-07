using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hl7.DAL.Migrations; 

/// <inheritdoc />
public partial class OrderNumberInt : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "OrderNumber",
            table: "MedicalRecord",
            type: "INTEGER",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OrderNumber",
            table: "MedicalRecord",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "INTEGER",
            oldNullable: true);
    }
}
