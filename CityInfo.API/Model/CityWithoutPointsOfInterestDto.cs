﻿using CityInfo.API.Enum;
using System.Text.Json.Serialization;

namespace CityInfo.API.Model
{
    public class CityWithoutPointsOfInterestDto
    {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ExpensiveCityType ExpensiveCity { get; set; }

        public string? Description { get; set; }
    }
}
