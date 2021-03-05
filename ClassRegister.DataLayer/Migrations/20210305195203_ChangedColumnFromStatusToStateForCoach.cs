using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRegister.DataLayer.Migrations
{
    public partial class ChangedColumnFromStatusToStateForCoach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Courses",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Courses",
                newName: "Status");
        }
    }
}
