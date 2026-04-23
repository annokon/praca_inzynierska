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
                name: "alcohol_preference",
                columns: table => new
                {
                    id_alcohol_preference = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alcohol_preference_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alcohol_preference", x => x.id_alcohol_preference);
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
                name: "driving_license",
                columns: table => new
                {
                    id_driving_license = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    driving_license_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driving_license", x => x.id_driving_license);
                });

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    id_gender = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gender_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.id_gender);
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
                    language_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.id_language);
                });

            migrationBuilder.CreateTable(
                name: "personality_type",
                columns: table => new
                {
                    id_personality_type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    personality_type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personality_type", x => x.id_personality_type);
                });

            migrationBuilder.CreateTable(
                name: "pronoun",
                columns: table => new
                {
                    id_pronoun = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pronoun_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pronoun", x => x.id_pronoun);
                });

            migrationBuilder.CreateTable(
                name: "smoking_preference",
                columns: table => new
                {
                    id_smoking_preference = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    smoking_preference_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smoking_preference", x => x.id_smoking_preference);
                });

            migrationBuilder.CreateTable(
                name: "transport_mode",
                columns: table => new
                {
                    id_transport_mode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transport_mode_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_mode", x => x.id_transport_mode);
                });

            migrationBuilder.CreateTable(
                name: "travel_experience",
                columns: table => new
                {
                    id_travel_experience = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    travel_experience_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travel_experience", x => x.id_travel_experience);
                });

            migrationBuilder.CreateTable(
                name: "travel_style",
                columns: table => new
                {
                    id_travel_style = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    travel_style_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travel_style", x => x.id_travel_style);
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
                    gender = table.Column<int>(type: "integer", nullable: true),
                    pronouns = table.Column<int>(type: "integer", nullable: true),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    personality_type = table.Column<int>(type: "integer", nullable: true),
                    alcohol_preference = table.Column<int>(type: "integer", nullable: true),
                    smoking_preference = table.Column<int>(type: "integer", nullable: true),
                    driving_license_type = table.Column<int>(type: "integer", nullable: true),
                    travel_experience = table.Column<int>(type: "integer", nullable: true),
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
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_user_alcohol_preference_alcohol_preference",
                        column: x => x.alcohol_preference,
                        principalTable: "alcohol_preference",
                        principalColumn: "id_alcohol_preference");
                    table.ForeignKey(
                        name: "FK_user_driving_license_driving_license_type",
                        column: x => x.driving_license_type,
                        principalTable: "driving_license",
                        principalColumn: "id_driving_license");
                    table.ForeignKey(
                        name: "FK_user_gender_gender",
                        column: x => x.gender,
                        principalTable: "gender",
                        principalColumn: "id_gender");
                    table.ForeignKey(
                        name: "FK_user_personality_type_personality_type",
                        column: x => x.personality_type,
                        principalTable: "personality_type",
                        principalColumn: "id_personality_type");
                    table.ForeignKey(
                        name: "FK_user_pronoun_pronouns",
                        column: x => x.pronouns,
                        principalTable: "pronoun",
                        principalColumn: "id_pronoun");
                    table.ForeignKey(
                        name: "FK_user_smoking_preference_smoking_preference",
                        column: x => x.smoking_preference,
                        principalTable: "smoking_preference",
                        principalColumn: "id_smoking_preference");
                    table.ForeignKey(
                        name: "FK_user_travel_experience_travel_experience",
                        column: x => x.travel_experience,
                        principalTable: "travel_experience",
                        principalColumn: "id_travel_experience");
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
                name: "user_travel_style",
                columns: table => new
                {
                    id_travel_style = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_travel_style", x => new { x.id_travel_style, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_travel_style_travel_style_id_travel_style",
                        column: x => x.id_travel_style,
                        principalTable: "travel_style",
                        principalColumn: "id_travel_style",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_travel_style_user_id_user",
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
                table: "alcohol_preference",
                columns: new[] { "id_alcohol_preference", "alcohol_preference_name" },
                values: new object[,]
                {
                    { 1, "Nie piję i nie przeszkadza mi" },
                    { 2, "Piję okazjonalnie" },
                    { 3, "Piję regularnie" },
                    { 4, "Nie toleruję" }
                });

            migrationBuilder.InsertData(
                table: "driving_license",
                columns: new[] { "id_driving_license", "driving_license_name" },
                values: new object[,]
                {
                    { 1, "Posiadam międzynarodowe" },
                    { 2, "Nie posiadam" },
                    { 3, "Inne" }
                });

            migrationBuilder.InsertData(
                table: "gender",
                columns: new[] { "id_gender", "gender_name" },
                values: new object[,]
                {
                    { 1, "Mężczyzna" },
                    { 2, "Kobieta" },
                    { 3, "Niebinarny" },
                    { 4, "Inne" }
                });

            migrationBuilder.InsertData(
                table: "personality_type",
                columns: new[] { "id_personality_type", "personality_type_name" },
                values: new object[,]
                {
                    { 1, "Introwertyk" },
                    { 2, "Ekstrawertyk" },
                    { 3, "Ambiwertyk" }
                });

            migrationBuilder.InsertData(
                table: "pronoun",
                columns: new[] { "id_pronoun", "pronoun_name" },
                values: new object[,]
                {
                    { 1, "On/Jego" },
                    { 2, "Ona/Jej" },
                    { 3, "Oni/Ich" }
                });

            migrationBuilder.InsertData(
                table: "smoking_preference",
                columns: new[] { "id_smoking_preference", "smoking_preference_name" },
                values: new object[,]
                {
                    { 1, "Nie palę i nie przeszkadza mi" },
                    { 2, "Palę okazjonalnie" },
                    { 3, "Palę" },
                    { 4, "Nie toleruję" }
                });

            migrationBuilder.InsertData(
                table: "transport_mode",
                columns: new[] { "id_transport_mode", "transport_mode_name" },
                values: new object[,]
                {
                    { 1, "Samochód" },
                    { 2, "Samolot" },
                    { 3, "Pociąg" },
                    { 4, "Autobus" },
                    { 5, "Rower" },
                    { 6, "Motor" },
                    { 7, "Prom" }
                });

            migrationBuilder.InsertData(
                table: "travel_experience",
                columns: new[] { "id_travel_experience", "travel_experience_name" },
                values: new object[,]
                {
                    { 1, "Początkujący" },
                    { 2, "Średniozaawansowany" },
                    { 3, "Zaawansowany" },
                    { 4, "Backpacker" }
                });

            migrationBuilder.InsertData(
                table: "travel_style",
                columns: new[] { "id_travel_style", "travel_style_name" },
                values: new object[,]
                {
                    { 1, "Spontaniczny" },
                    { 2, "Trochę zaplanowany" },
                    { 3, "Szczegółowo zaplanowany" },
                    { 4, "All-inclusive" },
                    { 5, "City break" },
                    { 6, "Road trip" },
                    { 7, "Wakacje z biurem podróży" },
                    { 8, "Podróże ekstremalne" },
                    { 9, "Slow travel" },
                    { 10, "Backpacking" }
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

            migrationBuilder.CreateIndex(
                name: "IX_user_travel_style_id_user",
                table: "user_travel_style",
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
                name: "user_travel_style");

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
                name: "travel_style");

            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "alcohol_preference");

            migrationBuilder.DropTable(
                name: "driving_license");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "personality_type");

            migrationBuilder.DropTable(
                name: "pronoun");

            migrationBuilder.DropTable(
                name: "smoking_preference");

            migrationBuilder.DropTable(
                name: "travel_experience");
        }
    }
}
