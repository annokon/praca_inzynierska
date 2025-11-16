using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Favourite
{
    [Key]
    public int IdFavourite { get; set; }

    // user adding to favourites
    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string IdUser { get; set; } = null!;
    public User User { get; set; } = null!;

    // user added to favourites
    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(FavouriteUser))]
    public string IdUserFavourite { get; set; } = null!;
    public User FavouriteUser { get; set; } = null!;
}