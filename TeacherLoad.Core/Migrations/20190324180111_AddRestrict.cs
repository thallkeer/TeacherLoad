using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherLoad.Core.Migrations
{
    public partial class AddRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupLoads_Disciplines_DisciplineID",
                table: "GroupLoads");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroup_Specialities_SpecialityCode",
                table: "StudyGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Positions_PositionID",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Positions_PositionID_PositionName",
                table: "Positions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonalStudies_IndividualClassName",
                table: "PersonalStudies");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonalLoads_TeacherID_IndividualClassID",
                table: "PersonalLoads");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GroupStudies_GroupClassName",
                table: "GroupStudies");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GroupLoads_TeacherID_GroupStudiesID_GroupNumber_DisciplineID_Semester_StudyType_StudyYear",
                table: "GroupLoads");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Disciplines_DisciplineName",
                table: "Disciplines");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PositionName",
                table: "Positions",
                column: "PositionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalStudies_IndividualClassName",
                table: "PersonalStudies",
                column: "IndividualClassName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLoads_TeacherID_IndividualClassID",
                table: "PersonalLoads",
                columns: new[] { "TeacherID", "IndividualClassID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudies_GroupClassName",
                table: "GroupStudies",
                column: "GroupClassName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupLoads_TeacherID_GroupStudiesID_GroupNumber_DisciplineID_Semester_StudyType_StudyYear",
                table: "GroupLoads",
                columns: new[] { "TeacherID", "GroupStudiesID", "GroupNumber", "DisciplineID", "Semester", "StudyType", "StudyYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DisciplineName",
                table: "Disciplines",
                column: "DisciplineName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupLoads_Disciplines_DisciplineID",
                table: "GroupLoads",
                column: "DisciplineID",
                principalTable: "Disciplines",
                principalColumn: "DisciplineID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroup_Specialities_SpecialityCode",
                table: "StudyGroup",
                column: "SpecialityCode",
                principalTable: "Specialities",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Positions_PositionID",
                table: "Teachers",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "PositionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupLoads_Disciplines_DisciplineID",
                table: "GroupLoads");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroup_Specialities_SpecialityCode",
                table: "StudyGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Positions_PositionID",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Positions_PositionName",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_PersonalStudies_IndividualClassName",
                table: "PersonalStudies");

            migrationBuilder.DropIndex(
                name: "IX_PersonalLoads_TeacherID_IndividualClassID",
                table: "PersonalLoads");

            migrationBuilder.DropIndex(
                name: "IX_GroupStudies_GroupClassName",
                table: "GroupStudies");

            migrationBuilder.DropIndex(
                name: "IX_GroupLoads_TeacherID_GroupStudiesID_GroupNumber_DisciplineID_Semester_StudyType_StudyYear",
                table: "GroupLoads");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_DisciplineName",
                table: "Disciplines");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonalStudies_IndividualClassName",
                table: "PersonalStudies",
                column: "IndividualClassName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonalLoads_TeacherID_IndividualClassID",
                table: "PersonalLoads",
                columns: new[] { "TeacherID", "IndividualClassID" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GroupStudies_GroupClassName",
                table: "GroupStudies",
                column: "GroupClassName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GroupLoads_TeacherID_GroupStudiesID_GroupNumber_DisciplineID_Semester_StudyType_StudyYear",
                table: "GroupLoads",
                columns: new[] { "TeacherID", "GroupStudiesID", "GroupNumber", "DisciplineID", "Semester", "StudyType", "StudyYear" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Disciplines_DisciplineName",
                table: "Disciplines",
                column: "DisciplineName");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PositionID_PositionName",
                table: "Positions",
                columns: new[] { "PositionID", "PositionName" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupLoads_Disciplines_DisciplineID",
                table: "GroupLoads",
                column: "DisciplineID",
                principalTable: "Disciplines",
                principalColumn: "DisciplineID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroup_Specialities_SpecialityCode",
                table: "StudyGroup",
                column: "SpecialityCode",
                principalTable: "Specialities",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Positions_PositionID",
                table: "Teachers",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "PositionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
