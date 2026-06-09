using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    AttendeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.AttendeeId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueDetails",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    ParkingSpaces = table.Column<int>(type: "int", nullable: false),
                    AccessibilityInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueDetails", x => x.VenueId);
                    table.ForeignKey(
                        name: "FK_VenueDetails_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    AttendeeId = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => new { x.EventId, x.AttendeeId });
                    table.ForeignKey(
                        name: "FK_Registrations_Attendees_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Attendees",
                        principalColumn: "AttendeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "AttendeeId", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "anna@email.com", "Anna", "Kowalska", "123456789" },
                    { 2, "jan@email.com", "Jan", "Nowak", "234567891" },
                    { 3, "maria@email.com", "Maria", "Wisniewska", "345678912" },
                    { 4, "piotr@email.com", "Piotr", "Zielinski", "456789123" },
                    { 5, "kasia@email.com", "Katarzyna", "Lewandowska", "567891234" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "Address", "Capacity", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Marszalkowska 12, Warsaw", 500, "National Concert Hall", "111222333" },
                    { 2, "Lazienki Park 1, Warsaw", 1000, "Open Air Stage", "222333444" },
                    { 3, "Nowy Swiat 45, Warsaw", 120, "Gallery Nova", "333444555" },
                    { 4, "Zlota 59, Warsaw", 300, "City Conference Center", "444555666" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventDate", "Name", "Status", "TicketPrice", "VenueId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), "Warsaw Jazz Festival", "Scheduled", 120.00m, 1 },
                    { 2, new DateTime(2026, 7, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), "Summer Film Night", "Scheduled", 45.00m, 2 },
                    { 3, new DateTime(2026, 6, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Modern Art Exhibition", "Completed", 30.00m, 3 },
                    { 4, new DateTime(2026, 8, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Tech Conference 2026", "Scheduled", 200.00m, 4 },
                    { 5, new DateTime(2026, 7, 25, 20, 0, 0, 0, DateTimeKind.Unspecified), "Classical Music Evening", "Cancelled", 80.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "VenueDetails",
                columns: new[] { "VenueId", "AccessibilityInfo", "ParkingSpaces", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, "Wheelchair ramps and elevator access", 150, "https://nch.example.com" },
                    { 2, "Flat terrain, accessible paths", 0, null },
                    { 3, "Ground floor only, no elevator", 30, "https://test321.example.com" },
                    { 4, "Full accessibility, hearing loop available", 200, "https://test123.example.com" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "AttendeeId", "EventId", "RegisteredAt", "SeatNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 20, 10, 15, 0, 0, DateTimeKind.Unspecified), "C14" },
                    { 2, 1, new DateTime(2026, 6, 21, 8, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 2, new DateTime(2026, 6, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 3, new DateTime(2026, 6, 15, 9, 45, 0, 0, DateTimeKind.Unspecified), "A1" },
                    { 1, 4, new DateTime(2026, 7, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "R4" },
                    { 5, 4, new DateTime(2026, 7, 1, 14, 20, 0, 0, DateTimeKind.Unspecified), "R3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_AttendeeId",
                table: "Registrations",
                column: "AttendeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "VenueDetails");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
