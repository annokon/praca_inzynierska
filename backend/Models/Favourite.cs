using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Favourite
{
    [Key]
    public int IdFavourite { get; set; }

    // user adding to favourites
    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    public User User { get; set; } = null!;

    // user added to favourites
    [ForeignKey(nameof(FavouriteUser))]
    public int? IdUserFavourite { get; set; }
    public User FavouriteUser { get; set; } = null!;
}