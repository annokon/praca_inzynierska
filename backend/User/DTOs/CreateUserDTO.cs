using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CreateUserDTO
{
    // required data
    [Required, MaxLength(20)]
    public int IdUser { get; set; }
    
    [Required, MaxLength(20)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(30)]
    public string DisplayName { get; set; } = null!;

    [Required, EmailAddress, MaxLength(320)]
    public string Email { get; set; } = null!;

    [Required, MinLength(12), MaxLength(100)]
    public string Password { get; set; } = null!;

    [Required]
    public DateOnly BirthDate { get; set; }

    [Required, MaxLength(20)]
    public string Gender { get; set; } = null!;

    
    // terms of use
    public bool AcceptTerms { get; set; } = false;
}