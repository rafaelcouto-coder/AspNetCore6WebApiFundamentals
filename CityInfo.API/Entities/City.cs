﻿using CityInfo.API.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

        [Required]
        public ExpensiveCityType ExpensiveCity { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
            = new List<PointOfInterest>();

        public City(string name)
        {
            Name = name;
        }
    }
}
