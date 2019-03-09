using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherLoad.Core.Migrations
{
    public partial class SmallChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "Patronym",
            //    table: "Teachers",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 50);

            //migrationBuilder.AlterColumn<string>(
            //    name: "LastName",
            //    table: "Teachers",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 50);

            //migrationBuilder.AlterColumn<string>(
            //    name: "FirstName",
            //    table: "Teachers",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 50);

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "VolumeByPerson",
            //    table: "PersonalStudies",
            //    nullable: false,
            //    oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Patronym",
                table: "Teachers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "VolumeByPerson",
                table: "PersonalStudies",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
