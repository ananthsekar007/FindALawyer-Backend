using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindALawyer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPAymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "razorpay_payments",
                newName: "PaymenrOrderId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "razorpay_payments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "razorpay_payments");

            migrationBuilder.RenameColumn(
                name: "PaymenrOrderId",
                table: "razorpay_payments",
                newName: "ReferenceId");
        }
    }
}
