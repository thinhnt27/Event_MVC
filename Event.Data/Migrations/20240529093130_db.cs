using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.Data.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:Event.Data/Migrations/20240529043648_InitialCreate.cs
    public partial class InitialCreate : Migration
========
    public partial class db : Migration
>>>>>>>> main:Event.Data/Migrations/20240529093130_db.cs
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64D850EC5690", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    EventTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventTyp__A9216B3FFEC0D566", x => x.EventTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EventTypeId = table.Column<int>(type: "int", nullable: true),
                    EventDescription = table.Column<string>(type: "text", nullable: true),
                    NumberTickets = table.Column<int>(type: "int", nullable: true),
                    Sponsor = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SubjectCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Events__7944C810032ACB17", x => x.EventId);
                    table.ForeignKey(
                        name: "FK__Events__EventTyp__5629CD9C",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "EventTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    VistorCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ParticipantName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ParticipantType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    AttendeeEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RegistrationPhone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FeePaid = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Checkin = table.Column<bool>(type: "bit", nullable: true),
                    CheckinTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Registra__6EF5881049808010", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK__Registrat__Custo__59063A47",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK__Registrat__Event__59FA5E80",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    TicketType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tickets__712CC607682BB742", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK__Tickets__EventId__5AEE82B9",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    TicketQuantity = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    PaymentType = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A38227246BE", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK__Payments__Regist__571DF1D5",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId");
                    table.ForeignKey(
                        name: "FK__Payments__Ticket__5812160E",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__5C7E359E5B77F523",
                table: "Customers",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__7A22C6EA0D7F776F",
                table: "Customers",
                column: "CustomerName",
                unique: true,
                filter: "[CustomerName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__A9D105345CFB149F",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RegistrationId",
                table: "Payments",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TicketId",
                table: "Payments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_CustomerId",
                table: "Registrations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventTypes");
        }
    }
}
