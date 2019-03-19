using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherLoad.Core.Migrations
{
    public partial class ChangeKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalLoads",
                table: "PersonalLoads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupLoads",
                table: "GroupLoads");

            migrationBuilder.AddColumn<int>(
                name: "PersonalLoadID",
                table: "PersonalLoads",
                nullable: false,
                defaultValue: 1)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "GroupLoadID",
                table: "GroupLoads",
                nullable: false,
                defaultValue: 1)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.AlterColumn<string>(
            //    name: "DisciplineName",
            //    table: "Disciplines",
            //    maxLength: 30,
            //    nullable: false,
            //    oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalLoads",
                table: "PersonalLoads",
                column: "PersonalLoadID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonalLoads_TeacherID_IndividualClassID",
                table: "PersonalLoads",
                columns: new[] { "TeacherID", "IndividualClassID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupLoads",
                table: "GroupLoads",
                column: "GroupLoadID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GroupLoads_TeacherID_GroupStudiesID_GroupNumber_DisciplineID_Semester_StudyType_StudyYear",
                table: "GroupLoads",
                columns: new[] { "TeacherID", "GroupStudiesID", "GroupNumber", "DisciplineID", "Semester", "StudyType", "StudyYear" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalLoads",
                table: "PersonalLoads");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonalLoads_TeacherID_IndividualClassID",
                table: "PersonalLoads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupLoads",
                table: "GroupLoads");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GroupLoads_TeacherID_GroupStudiesID_GroupNumber_DisciplineID_Semester_StudyType_StudyYear",
                table: "GroupLoads");

            migrationBuilder.DropColumn(
                name: "PersonalLoadID",
                table: "PersonalLoads");

            migrationBuilder.DropColumn(
                name: "GroupLoadID",
                table: "GroupLoads");

            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Disciplines",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalLoads",
                table: "PersonalLoads",
                columns: new[] { "TeacherID", "IndividualClassID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupLoads",
                table: "GroupLoads",
                columns: new[] { "TeacherID", "GroupStudiesID", "GroupNumber", "DisciplineID", "Semester", "StudyType", "StudyYear" });
        }
    }
}
