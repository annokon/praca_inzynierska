using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    BadgeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IconPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.BadgeId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    IdLanguage = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.IdLanguage);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    IdTrip = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DurationDays = table.Column<int>(type: "integer", nullable: true),
                    Destination = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BudgetAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    BudgetCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsReported = table.Column<bool>(type: "boolean", nullable: false),
                    TravelType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.IdTrip);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Pronouns = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PersonalityType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    AlcoholPreference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SmokingPreference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DrivingLicenseType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TravelStyle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TravelExperience = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProfilePhotoPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    BackgroundPhotoPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ToBeDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    SystemLanguage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    IdChat = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsGroupChat = table.Column<bool>(type: "boolean", nullable: false),
                    IdTrip = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.IdChat);
                    table.ForeignKey(
                        name: "FK_Chat_Trip_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trip",
                        principalColumn: "IdTrip");
                });

            migrationBuilder.CreateTable(
                name: "BlockedUser",
                columns: table => new
                {
                    IdUserBlocker = table.Column<int>(type: "integer", nullable: false),
                    IdUserBlocked = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUser", x => new { x.IdUserBlocker, x.IdUserBlocked });
                    table.ForeignKey(
                        name: "FK_BlockedUser_Users_IdUserBlocked",
                        column: x => x.IdUserBlocked,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlockedUser_Users_IdUserBlocker",
                        column: x => x.IdUserBlocker,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    IdFavourite = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: true),
                    IdUserFavourite = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.IdFavourite);
                    table.ForeignKey(
                        name: "FK_Favourite_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_Favourite_Users_IdUserFavourite",
                        column: x => x.IdUserFavourite,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    IdReview = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUserReviewer = table.Column<int>(type: "integer", nullable: true),
                    IdUserReviewed = table.Column<int>(type: "integer", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsReported = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.IdReview);
                    table.ForeignKey(
                        name: "FK_Review_Users_IdUserReviewed",
                        column: x => x.IdUserReviewed,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_Review_Users_IdUserReviewer",
                        column: x => x.IdUserReviewer,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "SearchFilter",
                columns: table => new
                {
                    IdSearchFilter = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Criteria = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchFilter", x => x.IdSearchFilter);
                    table.ForeignKey(
                        name: "FK_SearchFilter_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "TripInvitation",
                columns: table => new
                {
                    IdTripInvitation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    IdUserInviting = table.Column<int>(type: "integer", nullable: true),
                    IdUserInvited = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripInvitation", x => x.IdTripInvitation);
                    table.ForeignKey(
                        name: "FK_TripInvitation_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripInvitation_Users_IdUserInvited",
                        column: x => x.IdUserInvited,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_TripInvitation_Users_IdUserInviting",
                        column: x => x.IdUserInviting,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "TripPhoto",
                columns: table => new
                {
                    IdTripPhoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTrip = table.Column<int>(type: "integer", nullable: false),
                    UploadedBy = table.Column<int>(type: "integer", nullable: false),
                    PhotoPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPhoto", x => x.IdTripPhoto);
                    table.ForeignKey(
                        name: "FK_TripPhoto_Trip_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trip",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripPhoto_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBadge",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdBadge = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadge", x => new { x.IdUser, x.IdBadge });
                    table.ForeignKey(
                        name: "FK_UserBadge_Badge_IdBadge",
                        column: x => x.IdBadge,
                        principalTable: "Badge",
                        principalColumn: "BadgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBadge_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLanguage",
                columns: table => new
                {
                    IdLanguage = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLanguage", x => new { x.IdLanguage, x.IdUser });
                    table.ForeignKey(
                        name: "FK_UserLanguage_Language_IdLanguage",
                        column: x => x.IdLanguage,
                        principalTable: "Language",
                        principalColumn: "IdLanguage",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLanguage_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    IdMessage = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdChat = table.Column<int>(type: "integer", nullable: false),
                    IdUserSender = table.Column<int>(type: "integer", nullable: true),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    MediaUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.IdMessage);
                    table.ForeignKey(
                        name: "FK_Message_Chat_IdChat",
                        column: x => x.IdChat,
                        principalTable: "Chat",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Users_IdUserSender",
                        column: x => x.IdUserSender,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "UserChat",
                columns: table => new
                {
                    IdChat = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChat", x => new { x.IdChat, x.IdUser });
                    table.ForeignKey(
                        name: "FK_UserChat_Chat_IdChat",
                        column: x => x.IdChat,
                        principalTable: "Chat",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChat_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    IdNotification = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: true),
                    IdMessage = table.Column<int>(type: "integer", nullable: true),
                    IdTripInvitation = table.Column<int>(type: "integer", nullable: true),
                    IdReview = table.Column<int>(type: "integer", nullable: true),
                    ReviewIdReview = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.IdNotification);
                    table.ForeignKey(
                        name: "FK_Notification_Message_IdMessage",
                        column: x => x.IdMessage,
                        principalTable: "Message",
                        principalColumn: "IdMessage",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Review_ReviewIdReview",
                        column: x => x.ReviewIdReview,
                        principalTable: "Review",
                        principalColumn: "IdReview");
                    table.ForeignKey(
                        name: "FK_Notification_TripInvitation_IdTripInvitation",
                        column: x => x.IdTripInvitation,
                        principalTable: "TripInvitation",
                        principalColumn: "IdTripInvitation");
                    table.ForeignKey(
                        name: "FK_Notification_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUser_IdUserBlocked",
                table: "BlockedUser",
                column: "IdUserBlocked");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_IdTrip",
                table: "Chat",
                column: "IdTrip");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_IdUser",
                table: "Favourite",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_IdUserFavourite",
                table: "Favourite",
                column: "IdUserFavourite");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdChat",
                table: "Message",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdUserSender",
                table: "Message",
                column: "IdUserSender");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IdMessage",
                table: "Notification",
                column: "IdMessage");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IdTripInvitation",
                table: "Notification",
                column: "IdTripInvitation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IdUser",
                table: "Notification",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ReviewIdReview",
                table: "Notification",
                column: "ReviewIdReview");

            migrationBuilder.CreateIndex(
                name: "IX_Review_IdUserReviewed",
                table: "Review",
                column: "IdUserReviewed");

            migrationBuilder.CreateIndex(
                name: "IX_Review_IdUserReviewer",
                table: "Review",
                column: "IdUserReviewer");

            migrationBuilder.CreateIndex(
                name: "IX_SearchFilter_IdUser",
                table: "SearchFilter",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_TripInvitation_IdUserInvited",
                table: "TripInvitation",
                column: "IdUserInvited");

            migrationBuilder.CreateIndex(
                name: "IX_TripInvitation_IdUserInviting",
                table: "TripInvitation",
                column: "IdUserInviting");

            migrationBuilder.CreateIndex(
                name: "IX_TripInvitation_TripId",
                table: "TripInvitation",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPhoto_IdTrip",
                table: "TripPhoto",
                column: "IdTrip");

            migrationBuilder.CreateIndex(
                name: "IX_TripPhoto_UploadedBy",
                table: "TripPhoto",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadge_IdBadge",
                table: "UserBadge",
                column: "IdBadge");

            migrationBuilder.CreateIndex(
                name: "IX_UserChat_IdUser",
                table: "UserChat",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguage_IdUser",
                table: "UserLanguage",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedUser");

            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "SearchFilter");

            migrationBuilder.DropTable(
                name: "TripPhoto");

            migrationBuilder.DropTable(
                name: "UserBadge");

            migrationBuilder.DropTable(
                name: "UserChat");

            migrationBuilder.DropTable(
                name: "UserLanguage");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "TripInvitation");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trip");
        }
    }
}
