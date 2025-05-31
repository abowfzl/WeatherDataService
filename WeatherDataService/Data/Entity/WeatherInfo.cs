using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigipayTask.Data.Entity;

[Table("weather_info")]
[Index(nameof(CreatedDate), IsUnique = false)]
public class WeatherInfo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("created_date")]
    public required DateTime CreatedDate { get; set; }

    [Required]
    [Column("json_raw_data")]
    public required string JsonRawData { get; set; }
}
