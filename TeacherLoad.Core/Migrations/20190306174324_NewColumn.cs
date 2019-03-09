using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherLoad.Core.Migrations
{
    public partial class NewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationLevel",
                table: "Specialities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VolumeByPerson",
                table: "PersonalStudies",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "StudentsCount",
                table: "PersonalLoads",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VolumeHours",
                table: "GroupLoads",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationLevel",
                table: "Specialities");

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeByPerson",
                table: "PersonalStudies",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "StudentsCount",
                table: "PersonalLoads",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "VolumeHours",
                table: "GroupLoads",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);
        }
    }
}
