using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("favourite")]
public class Favourite
{
    [Column("id_favourite")]
    [Key]
    public int IdFavourite { get; set; }
    
    // user adding to favourites
    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    
    [InverseProperty(nameof(User.FavouritesGiven))]
    public User User { get; set; } = null!;

    // user added to favourites
    [Column("id_user_favourite")]
    [ForeignKey(nameof(FavouriteUser))]
    public int? IdUserFavourite { get; set; }
    [InverseProperty(nameof(User.FavouritesReceived))]
    public User FavouriteUser { get; set; } = null!;
}