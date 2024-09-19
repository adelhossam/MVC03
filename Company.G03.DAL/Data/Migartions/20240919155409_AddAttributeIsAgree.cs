using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.G03.DAL.Data.Migartions
{
    /// <inheritdoc />
    public partial class AddAttributeIsAgree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAggree",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAggree",
                table: "AspNetUsers");
        }
    }
}
