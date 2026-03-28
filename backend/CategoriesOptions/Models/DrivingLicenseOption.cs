using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("driving_license")]
public class DrivingLicenseOption
{
    [Column("id_driving_license")]
    [Key]
    public int IdDrivingLicense { get; set; }
    
    [Column("driving_license_name")]
    public string DrivingLicenseName { get; set; } = null!;
}