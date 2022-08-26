using CityInfo.API.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CityInfo.API.Model
{
    public class CityForCreatingDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a expansive city value")]
        public ExpensiveCityType ExpensiveCity { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
