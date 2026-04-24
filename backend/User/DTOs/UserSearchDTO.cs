namespace backend.User.DTOs;

public class UserSearchDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string? ProfilePhotoPath { get; set; }
}