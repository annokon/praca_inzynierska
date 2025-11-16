using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUser_Users_IdUserBlocked",
                table: "BlockedUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUser_Users_IdUserBlocker",
                table: "BlockedUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Trip_IdTrip",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Users_IdUser",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Users_IdUserFavourite",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Chat_IdChat",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Users_IdUserSender",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Message_IdMessage",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Review_ReviewIdReview",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_TripInvitation_IdTripInvitation",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_IdUser",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_IdUserReviewed",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_IdUserReviewer",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchFilter_Users_IdUser",
                table: "SearchFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_TripInvitation_Trip_TripId",
                table: "TripInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_TripInvitation_Users_IdUserInvited",
                table: "TripInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_TripInvitation_Users_IdUserInviting",
                table: "TripInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPhoto_Trip_IdTrip",
                table: "TripPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPhoto_Users_UploadedBy",
                table: "TripPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBadge_Badge_IdBadge",
                table: "UserBadge");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBadge_Users_IdUser",
                table: "UserBadge");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChat_Chat_IdChat",
                table: "UserChat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChat_Users_IdUser",
                table: "UserChat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguage_Language_IdLanguage",
                table: "UserLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguage_Users_IdUser",
                table: "UserLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badge",
                table: "Badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLanguage",
                table: "UserLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChat",
                table: "UserChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBadge",
                table: "UserBadge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripPhoto",
                table: "TripPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripInvitation",
                table: "TripInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchFilter",
                table: "SearchFilter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedUser",
                table: "BlockedUser");

            migrationBuilder.RenameTable(
                name: "Trip",
                newName: "trip");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "review");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "notification");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "message");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "language");

            migrationBuilder.RenameTable(
                name: "Favourite",
                newName: "favourite");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "chat");

            migrationBuilder.RenameTable(
                name: "Badge",
                newName: "badge");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "UserLanguage",
                newName: "user_language");

            migrationBuilder.RenameTable(
                name: "UserChat",
                newName: "user_chat");

            migrationBuilder.RenameTable(
                name: "UserBadge",
                newName: "user_badge");

            migrationBuilder.RenameTable(
                name: "TripPhoto",
                newName: "trip_photo");

            migrationBuilder.RenameTable(
                name: "TripInvitation",
                newName: "trip_invitation");

            migrationBuilder.RenameTable(
                name: "SearchFilter",
                newName: "search_filter");

            migrationBuilder.RenameTable(
                name: "BlockedUser",
                newName: "blocked_users");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "trip",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "trip",
                newName: "destination");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "trip",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "trip",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TravelType",
                table: "trip",
                newName: "travel_type");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "trip",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "IsReported",
                table: "trip",
                newName: "is_reported");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "trip",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "trip",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "DurationDays",
                table: "trip",
                newName: "duration_days");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "trip",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BudgetCurrency",
                table: "trip",
                newName: "budget_currency");

            migrationBuilder.RenameColumn(
                name: "BudgetAmount",
                table: "trip",
                newName: "budget_amount");

            migrationBuilder.RenameColumn(
                name: "IdTrip",
                table: "trip",
                newName: "id_trip");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "review",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "review",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "IsReported",
                table: "review",
                newName: "is_reported");

            migrationBuilder.RenameColumn(
                name: "IdUserReviewer",
                table: "review",
                newName: "id_user_reviewer");

            migrationBuilder.RenameColumn(
                name: "IdUserReviewed",
                table: "review",
                newName: "id_user_reviewed");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "review",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "IdReview",
                table: "review",
                newName: "id_review");

            migrationBuilder.RenameIndex(
                name: "IX_Review_IdUserReviewer",
                table: "review",
                newName: "IX_review_id_user_reviewer");

            migrationBuilder.RenameIndex(
                name: "IX_Review_IdUserReviewed",
                table: "review",
                newName: "IX_review_id_user_reviewed");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "notification",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "notification",
                newName: "is_read");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "notification",
                newName: "id_user");

            migrationBuilder.RenameColumn(
                name: "IdTripInvitation",
                table: "notification",
                newName: "id_trip_invitation");

            migrationBuilder.RenameColumn(
                name: "IdReview",
                table: "notification",
                newName: "id_review");

            migrationBuilder.RenameColumn(
                name: "IdMessage",
                table: "notification",
                newName: "id_message");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "notification",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "IdNotification",
                table: "notification",
                newName: "id_notification");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_ReviewIdReview",
                table: "notification",
                newName: "IX_notification_ReviewIdReview");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_IdUser",
                table: "notification",
                newName: "IX_notification_id_user");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_IdTripInvitation",
                table: "notification",
                newName: "IX_notification_id_trip_invitation");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_IdMessage",
                table: "notification",
                newName: "IX_notification_id_message");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "message",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "message",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "message",
                newName: "media_url");

            migrationBuilder.RenameColumn(
                name: "IdUserSender",
                table: "message",
                newName: "id_user_sender");

            migrationBuilder.RenameColumn(
                name: "IdMessage",
                table: "message",
                newName: "id_message");

            migrationBuilder.RenameIndex(
                name: "IX_Message_IdChat",
                table: "message",
                newName: "IX_message_IdChat");

            migrationBuilder.RenameIndex(
                name: "IX_Message_IdUserSender",
                table: "message",
                newName: "IX_message_id_user_sender");

            migrationBuilder.RenameColumn(
                name: "LanguageName",
                table: "language",
                newName: "language_name");

            migrationBuilder.RenameColumn(
                name: "IdLanguage",
                table: "language",
                newName: "id_language");

            migrationBuilder.RenameColumn(
                name: "IdUserFavourite",
                table: "favourite",
                newName: "id_user_favourite");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "favourite",
                newName: "id_user");

            migrationBuilder.RenameColumn(
                name: "IdFavourite",
                table: "favourite",
                newName: "id_favourite");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_IdUserFavourite",
                table: "favourite",
                newName: "IX_favourite_id_user_favourite");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_IdUser",
                table: "favourite",
                newName: "IX_favourite_id_user");

            migrationBuilder.RenameColumn(
                name: "IsGroupChat",
                table: "chat",
                newName: "is_group_chat");

            migrationBuilder.RenameColumn(
                name: "IdTrip",
                table: "chat",
                newName: "id_trip");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "chat",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "IdChat",
                table: "chat",
                newName: "id_chat");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_IdTrip",
                table: "chat",
                newName: "IX_chat_id_trip");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "badge",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "badge",
                newName: "name_badge");

            migrationBuilder.RenameColumn(
                name: "IconPath",
                table: "badge",
                newName: "icon_path");

            migrationBuilder.RenameColumn(
                name: "BadgeId",
                table: "badge",
                newName: "id_badge");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "user",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Pronouns",
                table: "user",
                newName: "pronouns");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "user",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "user",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "user",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "user",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "user",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TravelStyle",
                table: "user",
                newName: "travel_style");

            migrationBuilder.RenameColumn(
                name: "TravelExperience",
                table: "user",
                newName: "travel_experience");

            migrationBuilder.RenameColumn(
                name: "ToBeDeleted",
                table: "user",
                newName: "to_be_deleted");

            migrationBuilder.RenameColumn(
                name: "SystemLanguage",
                table: "user",
                newName: "system_language");

            migrationBuilder.RenameColumn(
                name: "SmokingPreference",
                table: "user",
                newName: "smoking_preference");

            migrationBuilder.RenameColumn(
                name: "ProfilePhotoPath",
                table: "user",
                newName: "profile_photo_path");

            migrationBuilder.RenameColumn(
                name: "PersonalityType",
                table: "user",
                newName: "personality_type");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "user",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "user",
                newName: "is_blocked");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "user",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "EmailVerified",
                table: "user",
                newName: "email_verified");

            migrationBuilder.RenameColumn(
                name: "DrivingLicenseType",
                table: "user",
                newName: "driving_license_type");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "user",
                newName: "display_name");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "user",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "user",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "user",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "BackgroundPhotoPath",
                table: "user",
                newName: "background_photo_path");

            migrationBuilder.RenameColumn(
                name: "AlcoholPreference",
                table: "user",
                newName: "alcohol_preference");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "user",
                newName: "id_user");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "user_language",
                newName: "id_user");

            migrationBuilder.RenameColumn(
                name: "IdLanguage",
                table: "user_language",
                newName: "id_language");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguage_IdUser",
                table: "user_language",
                newName: "IX_user_language_id_user");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "user_chat",
                newName: "id_user");

            migrationBuilder.RenameColumn(
                name: "IdChat",
                table: "user_chat",
                newName: "id_chat");

            migrationBuilder.RenameIndex(
                name: "IX_UserChat_IdUser",
                table: "user_chat",
                newName: "IX_user_chat_id_user");

            migrationBuilder.RenameColumn(
                name: "IdBadge",
                table: "user_badge",
                newName: "id_badge");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "user_badge",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "IX_UserBadge_IdBadge",
                table: "user_badge",
                newName: "IX_user_badge_id_badge");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "trip_photo",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "trip_photo",
                newName: "uploaded_by");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "trip_photo",
                newName: "uploaded_at");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "trip_photo",
                newName: "photo_path");

            migrationBuilder.RenameColumn(
                name: "IdTrip",
                table: "trip_photo",
                newName: "id_trip");

            migrationBuilder.RenameColumn(
                name: "IdTripPhoto",
                table: "trip_photo",
                newName: "id_trip_photo");

            migrationBuilder.RenameIndex(
                name: "IX_TripPhoto_UploadedBy",
                table: "trip_photo",
                newName: "IX_trip_photo_uploaded_by");

            migrationBuilder.RenameIndex(
                name: "IX_TripPhoto_IdTrip",
                table: "trip_photo",
                newName: "IX_trip_photo_id_trip");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "trip_invitation",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "trip_invitation",
                newName: "id_trip");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "trip_invitation",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "IdUserInviting",
                table: "trip_invitation",
                newName: "id_user_inviting");

            migrationBuilder.RenameColumn(
                name: "IdUserInvited",
                table: "trip_invitation",
                newName: "id_user_invited");

            migrationBuilder.RenameColumn(
                name: "IdTripInvitation",
                table: "trip_invitation",
                newName: "id_trip_invitation");

            migrationBuilder.RenameIndex(
                name: "IX_TripInvitation_TripId",
                table: "trip_invitation",
                newName: "IX_trip_invitation_id_trip");

            migrationBuilder.RenameIndex(
                name: "IX_TripInvitation_IdUserInviting",
                table: "trip_invitation",
                newName: "IX_trip_invitation_id_user_inviting");

            migrationBuilder.RenameIndex(
                name: "IX_TripInvitation_IdUserInvited",
                table: "trip_invitation",
                newName: "IX_trip_invitation_id_user_invited");

            migrationBuilder.RenameColumn(
                name: "Criteria",
                table: "search_filter",
                newName: "criteria");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "search_filter",
                newName: "name_search_filter");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "search_filter",
                newName: "id_user");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "search_filter",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "IdSearchFilter",
                table: "search_filter",
                newName: "id_search_filter");

            migrationBuilder.RenameIndex(
                name: "IX_SearchFilter_IdUser",
                table: "search_filter",
                newName: "IX_search_filter_id_user");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "blocked_users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "IdUserBlocked",
                table: "blocked_users",
                newName: "id_user_blocked");

            migrationBuilder.RenameColumn(
                name: "IdUserBlocker",
                table: "blocked_users",
                newName: "id_user_blocker");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUser_IdUserBlocked",
                table: "blocked_users",
                newName: "IX_blocked_users_id_user_blocked");

            migrationBuilder.AddColumn<int>(
                name: "id_chat",
                table: "message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trip",
                table: "trip",
                column: "id_trip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_review",
                table: "review",
                column: "id_review");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notification",
                table: "notification",
                column: "id_notification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_message",
                table: "message",
                column: "id_message");

            migrationBuilder.AddPrimaryKey(
                name: "PK_language",
                table: "language",
                column: "id_language");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favourite",
                table: "favourite",
                column: "id_favourite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chat",
                table: "chat",
                column: "id_chat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_badge",
                table: "badge",
                column: "id_badge");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id_user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_language",
                table: "user_language",
                columns: new[] { "id_language", "id_user" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_chat",
                table: "user_chat",
                columns: new[] { "id_chat", "id_user" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_badge",
                table: "user_badge",
                columns: new[] { "id_user", "id_badge" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_trip_photo",
                table: "trip_photo",
                column: "id_trip_photo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trip_invitation",
                table: "trip_invitation",
                column: "id_trip_invitation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_search_filter",
                table: "search_filter",
                column: "id_search_filter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blocked_users",
                table: "blocked_users",
                columns: new[] { "id_user_blocker", "id_user_blocked" });

            migrationBuilder.AddForeignKey(
                name: "FK_blocked_users_user_id_user_blocked",
                table: "blocked_users",
                column: "id_user_blocked",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_blocked_users_user_id_user_blocker",
                table: "blocked_users",
                column: "id_user_blocker",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_trip_id_trip",
                table: "chat",
                column: "id_trip",
                principalTable: "trip",
                principalColumn: "id_trip");

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_user_id_user",
                table: "favourite",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_user_id_user_favourite",
                table: "favourite",
                column: "id_user_favourite",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_message_chat_IdChat",
                table: "message",
                column: "IdChat",
                principalTable: "chat",
                principalColumn: "id_chat",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_message_user_id_user_sender",
                table: "message",
                column: "id_user_sender",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_message_id_message",
                table: "notification",
                column: "id_message",
                principalTable: "message",
                principalColumn: "id_message",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notification_review_ReviewIdReview",
                table: "notification",
                column: "ReviewIdReview",
                principalTable: "review",
                principalColumn: "id_review");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_trip_invitation_id_trip_invitation",
                table: "notification",
                column: "id_trip_invitation",
                principalTable: "trip_invitation",
                principalColumn: "id_trip_invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_user_id_user",
                table: "notification",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_review_user_id_user_reviewed",
                table: "review",
                column: "id_user_reviewed",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_review_user_id_user_reviewer",
                table: "review",
                column: "id_user_reviewer",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_search_filter_user_id_user",
                table: "search_filter",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_trip_invitation_trip_id_trip",
                table: "trip_invitation",
                column: "id_trip",
                principalTable: "trip",
                principalColumn: "id_trip",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_invitation_user_id_user_invited",
                table: "trip_invitation",
                column: "id_user_invited",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_trip_invitation_user_id_user_inviting",
                table: "trip_invitation",
                column: "id_user_inviting",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_trip_photo_trip_id_trip",
                table: "trip_photo",
                column: "id_trip",
                principalTable: "trip",
                principalColumn: "id_trip",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_photo_user_uploaded_by",
                table: "trip_photo",
                column: "uploaded_by",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_badge_badge_id_badge",
                table: "user_badge",
                column: "id_badge",
                principalTable: "badge",
                principalColumn: "id_badge",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_badge_user_id_user",
                table: "user_badge",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_chat_chat_id_chat",
                table: "user_chat",
                column: "id_chat",
                principalTable: "chat",
                principalColumn: "id_chat",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_chat_user_id_user",
                table: "user_chat",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_language_language_id_language",
                table: "user_language",
                column: "id_language",
                principalTable: "language",
                principalColumn: "id_language",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_language_user_id_user",
                table: "user_language",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blocked_users_user_id_user_blocked",
                table: "blocked_users");

            migrationBuilder.DropForeignKey(
                name: "FK_blocked_users_user_id_user_blocker",
                table: "blocked_users");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_trip_id_trip",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK_favourite_user_id_user",
                table: "favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_favourite_user_id_user_favourite",
                table: "favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_message_chat_IdChat",
                table: "message");

            migrationBuilder.DropForeignKey(
                name: "FK_message_user_id_user_sender",
                table: "message");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_message_id_message",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_review_ReviewIdReview",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_trip_invitation_id_trip_invitation",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_user_id_user",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "FK_review_user_id_user_reviewed",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_review_user_id_user_reviewer",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_search_filter_user_id_user",
                table: "search_filter");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_invitation_trip_id_trip",
                table: "trip_invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_invitation_user_id_user_invited",
                table: "trip_invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_invitation_user_id_user_inviting",
                table: "trip_invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_photo_trip_id_trip",
                table: "trip_photo");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_photo_user_uploaded_by",
                table: "trip_photo");

            migrationBuilder.DropForeignKey(
                name: "FK_user_badge_badge_id_badge",
                table: "user_badge");

            migrationBuilder.DropForeignKey(
                name: "FK_user_badge_user_id_user",
                table: "user_badge");

            migrationBuilder.DropForeignKey(
                name: "FK_user_chat_chat_id_chat",
                table: "user_chat");

            migrationBuilder.DropForeignKey(
                name: "FK_user_chat_user_id_user",
                table: "user_chat");

            migrationBuilder.DropForeignKey(
                name: "FK_user_language_language_id_language",
                table: "user_language");

            migrationBuilder.DropForeignKey(
                name: "FK_user_language_user_id_user",
                table: "user_language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trip",
                table: "trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_review",
                table: "review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notification",
                table: "notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_message",
                table: "message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_language",
                table: "language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favourite",
                table: "favourite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chat",
                table: "chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_badge",
                table: "badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_language",
                table: "user_language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_chat",
                table: "user_chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_badge",
                table: "user_badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trip_photo",
                table: "trip_photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trip_invitation",
                table: "trip_invitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_search_filter",
                table: "search_filter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blocked_users",
                table: "blocked_users");

            migrationBuilder.DropColumn(
                name: "id_chat",
                table: "message");

            migrationBuilder.RenameTable(
                name: "trip",
                newName: "Trip");

            migrationBuilder.RenameTable(
                name: "review",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "notification",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "message",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "language",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "favourite",
                newName: "Favourite");

            migrationBuilder.RenameTable(
                name: "chat",
                newName: "Chat");

            migrationBuilder.RenameTable(
                name: "badge",
                newName: "Badge");

            migrationBuilder.RenameTable(
                name: "user_language",
                newName: "UserLanguage");

            migrationBuilder.RenameTable(
                name: "user_chat",
                newName: "UserChat");

            migrationBuilder.RenameTable(
                name: "user_badge",
                newName: "UserBadge");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "trip_photo",
                newName: "TripPhoto");

            migrationBuilder.RenameTable(
                name: "trip_invitation",
                newName: "TripInvitation");

            migrationBuilder.RenameTable(
                name: "search_filter",
                newName: "SearchFilter");

            migrationBuilder.RenameTable(
                name: "blocked_users",
                newName: "BlockedUser");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Trip",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "destination",
                table: "Trip",
                newName: "Destination");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Trip",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Trip",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "travel_type",
                table: "Trip",
                newName: "TravelType");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Trip",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "is_reported",
                table: "Trip",
                newName: "IsReported");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Trip",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "Trip",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "duration_days",
                table: "Trip",
                newName: "DurationDays");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Trip",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "budget_currency",
                table: "Trip",
                newName: "BudgetCurrency");

            migrationBuilder.RenameColumn(
                name: "budget_amount",
                table: "Trip",
                newName: "BudgetAmount");

            migrationBuilder.RenameColumn(
                name: "id_trip",
                table: "Trip",
                newName: "IdTrip");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "Review",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Review",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "is_reported",
                table: "Review",
                newName: "IsReported");

            migrationBuilder.RenameColumn(
                name: "id_user_reviewer",
                table: "Review",
                newName: "IdUserReviewer");

            migrationBuilder.RenameColumn(
                name: "id_user_reviewed",
                table: "Review",
                newName: "IdUserReviewed");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Review",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id_review",
                table: "Review",
                newName: "IdReview");

            migrationBuilder.RenameIndex(
                name: "IX_review_id_user_reviewer",
                table: "Review",
                newName: "IX_Review_IdUserReviewer");

            migrationBuilder.RenameIndex(
                name: "IX_review_id_user_reviewed",
                table: "Review",
                newName: "IX_Review_IdUserReviewed");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Notification",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "is_read",
                table: "Notification",
                newName: "IsRead");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "Notification",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "id_trip_invitation",
                table: "Notification",
                newName: "IdTripInvitation");

            migrationBuilder.RenameColumn(
                name: "id_review",
                table: "Notification",
                newName: "IdReview");

            migrationBuilder.RenameColumn(
                name: "id_message",
                table: "Notification",
                newName: "IdMessage");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Notification",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id_notification",
                table: "Notification",
                newName: "IdNotification");

            migrationBuilder.RenameIndex(
                name: "IX_notification_ReviewIdReview",
                table: "Notification",
                newName: "IX_Notification_ReviewIdReview");

            migrationBuilder.RenameIndex(
                name: "IX_notification_id_user",
                table: "Notification",
                newName: "IX_Notification_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_notification_id_trip_invitation",
                table: "Notification",
                newName: "IX_Notification_IdTripInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_notification_id_message",
                table: "Notification",
                newName: "IX_Notification_IdMessage");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "Message",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "Message",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "media_url",
                table: "Message",
                newName: "MediaUrl");

            migrationBuilder.RenameColumn(
                name: "id_user_sender",
                table: "Message",
                newName: "IdUserSender");

            migrationBuilder.RenameColumn(
                name: "id_message",
                table: "Message",
                newName: "IdMessage");

            migrationBuilder.RenameIndex(
                name: "IX_message_IdChat",
                table: "Message",
                newName: "IX_Message_IdChat");

            migrationBuilder.RenameIndex(
                name: "IX_message_id_user_sender",
                table: "Message",
                newName: "IX_Message_IdUserSender");

            migrationBuilder.RenameColumn(
                name: "language_name",
                table: "Language",
                newName: "LanguageName");

            migrationBuilder.RenameColumn(
                name: "id_language",
                table: "Language",
                newName: "IdLanguage");

            migrationBuilder.RenameColumn(
                name: "id_user_favourite",
                table: "Favourite",
                newName: "IdUserFavourite");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "Favourite",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "id_favourite",
                table: "Favourite",
                newName: "IdFavourite");

            migrationBuilder.RenameIndex(
                name: "IX_favourite_id_user_favourite",
                table: "Favourite",
                newName: "IX_Favourite_IdUserFavourite");

            migrationBuilder.RenameIndex(
                name: "IX_favourite_id_user",
                table: "Favourite",
                newName: "IX_Favourite_IdUser");

            migrationBuilder.RenameColumn(
                name: "is_group_chat",
                table: "Chat",
                newName: "IsGroupChat");

            migrationBuilder.RenameColumn(
                name: "id_trip",
                table: "Chat",
                newName: "IdTrip");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Chat",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id_chat",
                table: "Chat",
                newName: "IdChat");

            migrationBuilder.RenameIndex(
                name: "IX_chat_id_trip",
                table: "Chat",
                newName: "IX_Chat_IdTrip");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Badge",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "name_badge",
                table: "Badge",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "icon_path",
                table: "Badge",
                newName: "IconPath");

            migrationBuilder.RenameColumn(
                name: "id_badge",
                table: "Badge",
                newName: "BadgeId");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "UserLanguage",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "id_language",
                table: "UserLanguage",
                newName: "IdLanguage");

            migrationBuilder.RenameIndex(
                name: "IX_user_language_id_user",
                table: "UserLanguage",
                newName: "IX_UserLanguage_IdUser");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "UserChat",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "id_chat",
                table: "UserChat",
                newName: "IdChat");

            migrationBuilder.RenameIndex(
                name: "IX_user_chat_id_user",
                table: "UserChat",
                newName: "IX_UserChat_IdUser");

            migrationBuilder.RenameColumn(
                name: "id_badge",
                table: "UserBadge",
                newName: "IdBadge");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "UserBadge",
                newName: "IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_user_badge_id_badge",
                table: "UserBadge",
                newName: "IX_UserBadge_IdBadge");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "pronouns",
                table: "Users",
                newName: "Pronouns");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Users",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "Users",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "Users",
                newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "travel_style",
                table: "Users",
                newName: "TravelStyle");

            migrationBuilder.RenameColumn(
                name: "travel_experience",
                table: "Users",
                newName: "TravelExperience");

            migrationBuilder.RenameColumn(
                name: "to_be_deleted",
                table: "Users",
                newName: "ToBeDeleted");

            migrationBuilder.RenameColumn(
                name: "system_language",
                table: "Users",
                newName: "SystemLanguage");

            migrationBuilder.RenameColumn(
                name: "smoking_preference",
                table: "Users",
                newName: "SmokingPreference");

            migrationBuilder.RenameColumn(
                name: "profile_photo_path",
                table: "Users",
                newName: "ProfilePhotoPath");

            migrationBuilder.RenameColumn(
                name: "personality_type",
                table: "Users",
                newName: "PersonalityType");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "is_blocked",
                table: "Users",
                newName: "IsBlocked");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "email_verified",
                table: "Users",
                newName: "EmailVerified");

            migrationBuilder.RenameColumn(
                name: "driving_license_type",
                table: "Users",
                newName: "DrivingLicenseType");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "Users",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "Users",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Users",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "background_photo_path",
                table: "Users",
                newName: "BackgroundPhotoPath");

            migrationBuilder.RenameColumn(
                name: "alcohol_preference",
                table: "Users",
                newName: "AlcoholPreference");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "Users",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "TripPhoto",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "uploaded_by",
                table: "TripPhoto",
                newName: "UploadedBy");

            migrationBuilder.RenameColumn(
                name: "uploaded_at",
                table: "TripPhoto",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "photo_path",
                table: "TripPhoto",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "id_trip",
                table: "TripPhoto",
                newName: "IdTrip");

            migrationBuilder.RenameColumn(
                name: "id_trip_photo",
                table: "TripPhoto",
                newName: "IdTripPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_trip_photo_uploaded_by",
                table: "TripPhoto",
                newName: "IX_TripPhoto_UploadedBy");

            migrationBuilder.RenameIndex(
                name: "IX_trip_photo_id_trip",
                table: "TripPhoto",
                newName: "IX_TripPhoto_IdTrip");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "TripInvitation",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "TripInvitation",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "id_user_inviting",
                table: "TripInvitation",
                newName: "IdUserInviting");

            migrationBuilder.RenameColumn(
                name: "id_user_invited",
                table: "TripInvitation",
                newName: "IdUserInvited");

            migrationBuilder.RenameColumn(
                name: "id_trip",
                table: "TripInvitation",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "id_trip_invitation",
                table: "TripInvitation",
                newName: "IdTripInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_trip_invitation_id_user_inviting",
                table: "TripInvitation",
                newName: "IX_TripInvitation_IdUserInviting");

            migrationBuilder.RenameIndex(
                name: "IX_trip_invitation_id_user_invited",
                table: "TripInvitation",
                newName: "IX_TripInvitation_IdUserInvited");

            migrationBuilder.RenameIndex(
                name: "IX_trip_invitation_id_trip",
                table: "TripInvitation",
                newName: "IX_TripInvitation_TripId");

            migrationBuilder.RenameColumn(
                name: "criteria",
                table: "SearchFilter",
                newName: "Criteria");

            migrationBuilder.RenameColumn(
                name: "name_search_filter",
                table: "SearchFilter",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "SearchFilter",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "SearchFilter",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id_search_filter",
                table: "SearchFilter",
                newName: "IdSearchFilter");

            migrationBuilder.RenameIndex(
                name: "IX_search_filter_id_user",
                table: "SearchFilter",
                newName: "IX_SearchFilter_IdUser");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "BlockedUser",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id_user_blocked",
                table: "BlockedUser",
                newName: "IdUserBlocked");

            migrationBuilder.RenameColumn(
                name: "id_user_blocker",
                table: "BlockedUser",
                newName: "IdUserBlocker");

            migrationBuilder.RenameIndex(
                name: "IX_blocked_users_id_user_blocked",
                table: "BlockedUser",
                newName: "IX_BlockedUser_IdUserBlocked");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                table: "Trip",
                column: "IdTrip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "IdReview");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "IdNotification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "IdMessage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "IdLanguage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite",
                column: "IdFavourite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "IdChat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badge",
                table: "Badge",
                column: "BadgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLanguage",
                table: "UserLanguage",
                columns: new[] { "IdLanguage", "IdUser" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChat",
                table: "UserChat",
                columns: new[] { "IdChat", "IdUser" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBadge",
                table: "UserBadge",
                columns: new[] { "IdUser", "IdBadge" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripPhoto",
                table: "TripPhoto",
                column: "IdTripPhoto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripInvitation",
                table: "TripInvitation",
                column: "IdTripInvitation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchFilter",
                table: "SearchFilter",
                column: "IdSearchFilter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedUser",
                table: "BlockedUser",
                columns: new[] { "IdUserBlocker", "IdUserBlocked" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUser_Users_IdUserBlocked",
                table: "BlockedUser",
                column: "IdUserBlocked",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUser_Users_IdUserBlocker",
                table: "BlockedUser",
                column: "IdUserBlocker",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Trip_IdTrip",
                table: "Chat",
                column: "IdTrip",
                principalTable: "Trip",
                principalColumn: "IdTrip");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Users_IdUser",
                table: "Favourite",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Users_IdUserFavourite",
                table: "Favourite",
                column: "IdUserFavourite",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Chat_IdChat",
                table: "Message",
                column: "IdChat",
                principalTable: "Chat",
                principalColumn: "IdChat",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Users_IdUserSender",
                table: "Message",
                column: "IdUserSender",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Message_IdMessage",
                table: "Notification",
                column: "IdMessage",
                principalTable: "Message",
                principalColumn: "IdMessage",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Review_ReviewIdReview",
                table: "Notification",
                column: "ReviewIdReview",
                principalTable: "Review",
                principalColumn: "IdReview");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_TripInvitation_IdTripInvitation",
                table: "Notification",
                column: "IdTripInvitation",
                principalTable: "TripInvitation",
                principalColumn: "IdTripInvitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_IdUser",
                table: "Notification",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_IdUserReviewed",
                table: "Review",
                column: "IdUserReviewed",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_IdUserReviewer",
                table: "Review",
                column: "IdUserReviewer",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchFilter_Users_IdUser",
                table: "SearchFilter",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TripInvitation_Trip_TripId",
                table: "TripInvitation",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "IdTrip",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripInvitation_Users_IdUserInvited",
                table: "TripInvitation",
                column: "IdUserInvited",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TripInvitation_Users_IdUserInviting",
                table: "TripInvitation",
                column: "IdUserInviting",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TripPhoto_Trip_IdTrip",
                table: "TripPhoto",
                column: "IdTrip",
                principalTable: "Trip",
                principalColumn: "IdTrip",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPhoto_Users_UploadedBy",
                table: "TripPhoto",
                column: "UploadedBy",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadge_Badge_IdBadge",
                table: "UserBadge",
                column: "IdBadge",
                principalTable: "Badge",
                principalColumn: "BadgeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadge_Users_IdUser",
                table: "UserBadge",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChat_Chat_IdChat",
                table: "UserChat",
                column: "IdChat",
                principalTable: "Chat",
                principalColumn: "IdChat",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChat_Users_IdUser",
                table: "UserChat",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguage_Language_IdLanguage",
                table: "UserLanguage",
                column: "IdLanguage",
                principalTable: "Language",
                principalColumn: "IdLanguage",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguage_Users_IdUser",
                table: "UserLanguage",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
