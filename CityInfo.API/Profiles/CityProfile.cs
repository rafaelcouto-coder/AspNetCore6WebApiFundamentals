using AutoMapper;
using CityInfo.API.Model;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, CityWithoutPointsOfInterestDto>();
            CreateMap<Entities.City, CityDto>();
            CreateMap<CityForCreatingDto, Entities.City>();
            CreateMap<Entities.City, CityWithoutPointsOfInterestDto> ();
        }
    }
}
