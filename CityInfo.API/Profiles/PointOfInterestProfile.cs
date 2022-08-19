using AutoMapper;
using CityInfo.API.Model;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, PointOfInterestDto>();
        }
    }
}
