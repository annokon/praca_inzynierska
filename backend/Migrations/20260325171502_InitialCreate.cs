using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlcoholPreferenceOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcoholPreferenceOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "badge",
                columns: table => new
                {
                    id_badge = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_badge = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    icon_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_badge", x => x.id_badge);
                });

            migrationBuilder.CreateTable(
                name: "DrivingLicenseOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingLicenseOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenderOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interest",
                columns: table => new
                {
                    id_interest = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    interest_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interest", x => x.id_interest);
                });

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    id_language = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    language_name_pl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    language_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.id_language);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTypeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTypeOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PronounOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PronounOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmokingPreferenceOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmokingPreferenceOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transport_mode",
                columns: table => new
                {
                    id_transport_mode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transport_mode_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    transport_mode_name_pl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_mode", x => x.id_transport_mode);
                });

            migrationBuilder.CreateTable(
                name: "TravelExperienceOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NamePl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelExperienceOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    id_trip = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    duration_days = table.Column<int>(type: "integer", nullable: false),
                    destination = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    budget_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    budget_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_reported = table.Column<bool>(type: "boolean", nullable: false),
                    travel_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.id_trip);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    display_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    pronouns = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    personality_type = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    alcohol_preference = table.Column<int>(type: "integer", maxLength: 50, nullable: true),
                    smoking_preference = table.Column<int>(type: "integer", maxLength: 50, nullable: true),
                    driving_license_type = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    travel_style = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    travel_experience = table.Column<int>(type: "integer", maxLength: 50, nullable: true),
                    bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    profile_photo_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    background_photo_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    email_verified = table.Column<bool>(type: "boolean", nullable: false),
                    email_verification_code_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email_verification_expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email_verification_attempts = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    to_be_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    delete_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_blocked = table.Column<bool>(type: "boolean", nullable: false),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    system_language = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_user_AlcoholPreferenceOptions_alcohol_preference",
                        column: x => x.alcohol_preference,
                        principalTable: "AlcoholPreferenceOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_DrivingLicenseOptions_driving_license_type",
                        column: x => x.driving_license_type,
                        principalTable: "DrivingLicenseOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_GenderOptions_gender",
                        column: x => x.gender,
                        principalTable: "GenderOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_PersonalityTypeOptions_personality_type",
                        column: x => x.personality_type,
                        principalTable: "PersonalityTypeOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_PronounOptions_pronouns",
                        column: x => x.pronouns,
                        principalTable: "PronounOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_SmokingPreferenceOptions_smoking_preference",
                        column: x => x.smoking_preference,
                        principalTable: "SmokingPreferenceOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_TravelExperienceOptions_travel_experience",
                        column: x => x.travel_experience,
                        principalTable: "TravelExperienceOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "chat",
                columns: table => new
                {
                    id_chat = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_group_chat = table.Column<bool>(type: "boolean", nullable: false),
                    id_trip = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat", x => x.id_chat);
                    table.ForeignKey(
                        name: "FK_chat_trip_id_trip",
                        column: x => x.id_trip,
                        principalTable: "trip",
                        principalColumn: "id_trip");
                });

            migrationBuilder.CreateTable(
                name: "blocked_users",
                columns: table => new
                {
                    id_user_blocker = table.Column<int>(type: "integer", nullable: false),
                    id_user_blocked = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blocked_users", x => new { x.id_user_blocker, x.id_user_blocked });
                    table.ForeignKey(
                        name: "FK_blocked_users_user_id_user_blocked",
                        column: x => x.id_user_blocked,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blocked_users_user_id_user_blocker",
                        column: x => x.id_user_blocker,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favourite",
                columns: table => new
                {
                    id_favourite = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: true),
                    id_user_favourite = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourite", x => x.id_favourite);
                    table.ForeignKey(
                        name: "FK_favourite_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user");
                    table.ForeignKey(
                        name: "FK_favourite_user_id_user_favourite",
                        column: x => x.id_user_favourite,
                        principalTable: "user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    id_review = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user_reviewer = table.Column<int>(type: "integer", nullable: true),
                    id_user_reviewed = table.Column<int>(type: "integer", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_reported = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.id_review);
                    table.ForeignKey(
                        name: "FK_review_user_id_user_reviewed",
                        column: x => x.id_user_reviewed,
                        principalTable: "user",
                        principalColumn: "id_user");
                    table.ForeignKey(
                        name: "FK_review_user_id_user_reviewer",
                        column: x => x.id_user_reviewer,
                        principalTable: "user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "search_filter",
                columns: table => new
                {
                    id_search_filter = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: true),
                    name_search_filter = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    criteria = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_search_filter", x => x.id_search_filter);
                    table.ForeignKey(
                        name: "FK_search_filter_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "trip_invitation",
                columns: table => new
                {
                    id_trip_invitation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_trip = table.Column<int>(type: "integer", nullable: false),
                    id_user_inviting = table.Column<int>(type: "integer", nullable: true),
                    id_user_invited = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_invitation", x => x.id_trip_invitation);
                    table.ForeignKey(
                        name: "FK_trip_invitation_trip_id_trip",
                        column: x => x.id_trip,
                        principalTable: "trip",
                        principalColumn: "id_trip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_invitation_user_id_user_invited",
                        column: x => x.id_user_invited,
                        principalTable: "user",
                        principalColumn: "id_user");
                    table.ForeignKey(
                        name: "FK_trip_invitation_user_id_user_inviting",
                        column: x => x.id_user_inviting,
                        principalTable: "user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "trip_photo",
                columns: table => new
                {
                    id_trip_photo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_trip = table.Column<int>(type: "integer", nullable: false),
                    uploaded_by = table.Column<int>(type: "integer", nullable: false),
                    photo_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_photo", x => x.id_trip_photo);
                    table.ForeignKey(
                        name: "FK_trip_photo_trip_id_trip",
                        column: x => x.id_trip,
                        principalTable: "trip",
                        principalColumn: "id_trip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_photo_user_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_badge",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_badge = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_badge", x => new { x.id_user, x.id_badge });
                    table.ForeignKey(
                        name: "FK_user_badge_badge_id_badge",
                        column: x => x.id_badge,
                        principalTable: "badge",
                        principalColumn: "id_badge",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_badge_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_interest",
                columns: table => new
                {
                    id_interest = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_interest", x => new { x.id_interest, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_interest_interest_id_interest",
                        column: x => x.id_interest,
                        principalTable: "interest",
                        principalColumn: "id_interest",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_interest_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_language",
                columns: table => new
                {
                    id_language = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_language", x => new { x.id_language, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_language_language_id_language",
                        column: x => x.id_language,
                        principalTable: "language",
                        principalColumn: "id_language",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_language_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_transport_mode",
                columns: table => new
                {
                    id_transport_mode = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_transport_mode", x => new { x.id_transport_mode, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_transport_mode_transport_mode_id_transport_mode",
                        column: x => x.id_transport_mode,
                        principalTable: "transport_mode",
                        principalColumn: "id_transport_mode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_transport_mode_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    id_message = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_chat = table.Column<int>(type: "integer", nullable: false),
                    IdChat = table.Column<int>(type: "integer", nullable: false),
                    id_user_sender = table.Column<int>(type: "integer", nullable: true),
                    text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    media_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.id_message);
                    table.ForeignKey(
                        name: "FK_message_chat_IdChat",
                        column: x => x.IdChat,
                        principalTable: "chat",
                        principalColumn: "id_chat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_message_user_id_user_sender",
                        column: x => x.id_user_sender,
                        principalTable: "user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "user_chat",
                columns: table => new
                {
                    id_chat = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_chat", x => new { x.id_chat, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_chat_chat_id_chat",
                        column: x => x.id_chat,
                        principalTable: "chat",
                        principalColumn: "id_chat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_chat_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id_notification = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: true),
                    id_message = table.Column<int>(type: "integer", nullable: true),
                    id_trip_invitation = table.Column<int>(type: "integer", nullable: true),
                    id_review = table.Column<int>(type: "integer", nullable: true),
                    ReviewIdReview = table.Column<int>(type: "integer", nullable: true),
                    type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    is_read = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.id_notification);
                    table.ForeignKey(
                        name: "FK_notification_message_id_message",
                        column: x => x.id_message,
                        principalTable: "message",
                        principalColumn: "id_message",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notification_review_ReviewIdReview",
                        column: x => x.ReviewIdReview,
                        principalTable: "review",
                        principalColumn: "id_review");
                    table.ForeignKey(
                        name: "FK_notification_trip_invitation_id_trip_invitation",
                        column: x => x.id_trip_invitation,
                        principalTable: "trip_invitation",
                        principalColumn: "id_trip_invitation");
                    table.ForeignKey(
                        name: "FK_notification_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user");
                });

            migrationBuilder.InsertData(
                table: "AlcoholPreferenceOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "Does not drink but does not mind", "Nie piję i nie przeszkadza mi" },
                    { 2, "Social drinker", "Piję okazjonalnie" },
                    { 3, "Regular drinker", "Piję regularnie" },
                    { 4, "Does not tolerate alcohol", "Nie toleruję" }
                });

            migrationBuilder.InsertData(
                table: "DrivingLicenseOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "None", "Brak" },
                    { 2, "Yes, international", "Tak, międzynarodowe" }
                });

            migrationBuilder.InsertData(
                table: "GenderOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "Male", "Mężczyzna" },
                    { 2, "Female", "Kobieta" },
                    { 3, "Non-binary", "Niebinarny" },
                    { 4, "Other", "Inne" }
                });

            migrationBuilder.InsertData(
                table: "PersonalityTypeOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "Introvert", "Introwertyk" },
                    { 2, "Extrovert", "Ekstrawertyk" },
                    { 3, "Ambivert", "Ambiwertyk" }
                });

            migrationBuilder.InsertData(
                table: "PronounOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "He/Him", "On/Jego" },
                    { 2, "She/Her", "Ona/Jej" },
                    { 3, "They/Them", "Oni/Ich" }
                });

            migrationBuilder.InsertData(
                table: "SmokingPreferenceOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "Non-smoker but does not mind", "Nie palę i nie przeszkadza mi" },
                    { 2, "Occasional smoker", "Palę okazjonalnie" },
                    { 3, "Smoker", "Palę" },
                    { 4, "Does not tolerate smoking", "Nie toleruję" }
                });

            migrationBuilder.InsertData(
                table: "TravelExperienceOptions",
                columns: new[] { "Id", "NameEn", "NamePl" },
                values: new object[,]
                {
                    { 1, "Beginner", "Początkujący" },
                    { 2, "Intermediate", "Średniozaawansowany" },
                    { 3, "Experienced", "Zaawansowany" },
                    { 4, "Backpacker", "Backpacker" }
                });

            migrationBuilder.InsertData(
                table: "transport_mode",
                columns: new[] { "id_transport_mode", "transport_mode_name_en", "transport_mode_name_pl" },
                values: new object[,]
                {
                    { 1, "Car", "Samochód" },
                    { 2, "Plane", "Samolot" },
                    { 3, "Train", "Pociąg" },
                    { 4, "Bus", "Autobus" },
                    { 5, "Bike", "Rower" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_blocked_users_id_user_blocked",
                table: "blocked_users",
                column: "id_user_blocked");

            migrationBuilder.CreateIndex(
                name: "IX_chat_id_trip",
                table: "chat",
                column: "id_trip");

            migrationBuilder.CreateIndex(
                name: "IX_favourite_id_user",
                table: "favourite",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_favourite_id_user_favourite",
                table: "favourite",
                column: "id_user_favourite");

            migrationBuilder.CreateIndex(
                name: "IX_message_id_user_sender",
                table: "message",
                column: "id_user_sender");

            migrationBuilder.CreateIndex(
                name: "IX_message_IdChat",
                table: "message",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_notification_id_message",
                table: "notification",
                column: "id_message");

            migrationBuilder.CreateIndex(
                name: "IX_notification_id_trip_invitation",
                table: "notification",
                column: "id_trip_invitation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_id_user",
                table: "notification",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_notification_ReviewIdReview",
                table: "notification",
                column: "ReviewIdReview");

            migrationBuilder.CreateIndex(
                name: "IX_review_id_user_reviewed",
                table: "review",
                column: "id_user_reviewed");

            migrationBuilder.CreateIndex(
                name: "IX_review_id_user_reviewer",
                table: "review",
                column: "id_user_reviewer");

            migrationBuilder.CreateIndex(
                name: "IX_search_filter_id_user",
                table: "search_filter",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_trip_invitation_id_trip",
                table: "trip_invitation",
                column: "id_trip");

            migrationBuilder.CreateIndex(
                name: "IX_trip_invitation_id_user_invited",
                table: "trip_invitation",
                column: "id_user_invited");

            migrationBuilder.CreateIndex(
                name: "IX_trip_invitation_id_user_inviting",
                table: "trip_invitation",
                column: "id_user_inviting");

            migrationBuilder.CreateIndex(
                name: "IX_trip_photo_id_trip",
                table: "trip_photo",
                column: "id_trip");

            migrationBuilder.CreateIndex(
                name: "IX_trip_photo_uploaded_by",
                table: "trip_photo",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_alcohol_preference",
                table: "user",
                column: "alcohol_preference");

            migrationBuilder.CreateIndex(
                name: "IX_user_driving_license_type",
                table: "user",
                column: "driving_license_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_gender",
                table: "user",
                column: "gender");

            migrationBuilder.CreateIndex(
                name: "IX_user_personality_type",
                table: "user",
                column: "personality_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_pronouns",
                table: "user",
                column: "pronouns");

            migrationBuilder.CreateIndex(
                name: "IX_user_smoking_preference",
                table: "user",
                column: "smoking_preference");

            migrationBuilder.CreateIndex(
                name: "IX_user_travel_experience",
                table: "user",
                column: "travel_experience");

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_badge_id_badge",
                table: "user_badge",
                column: "id_badge");

            migrationBuilder.CreateIndex(
                name: "IX_user_chat_id_user",
                table: "user_chat",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_interest_id_user",
                table: "user_interest",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_language_id_user",
                table: "user_language",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_transport_mode_id_user",
                table: "user_transport_mode",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blocked_users");

            migrationBuilder.DropTable(
                name: "favourite");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "search_filter");

            migrationBuilder.DropTable(
                name: "trip_photo");

            migrationBuilder.DropTable(
                name: "user_badge");

            migrationBuilder.DropTable(
                name: "user_chat");

            migrationBuilder.DropTable(
                name: "user_interest");

            migrationBuilder.DropTable(
                name: "user_language");

            migrationBuilder.DropTable(
                name: "user_transport_mode");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "trip_invitation");

            migrationBuilder.DropTable(
                name: "badge");

            migrationBuilder.DropTable(
                name: "interest");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "transport_mode");

            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "AlcoholPreferenceOptions");

            migrationBuilder.DropTable(
                name: "DrivingLicenseOptions");

            migrationBuilder.DropTable(
                name: "GenderOptions");

            migrationBuilder.DropTable(
                name: "PersonalityTypeOptions");

            migrationBuilder.DropTable(
                name: "PronounOptions");

            migrationBuilder.DropTable(
                name: "SmokingPreferenceOptions");

            migrationBuilder.DropTable(
                name: "TravelExperienceOptions");
        }
    }
}
