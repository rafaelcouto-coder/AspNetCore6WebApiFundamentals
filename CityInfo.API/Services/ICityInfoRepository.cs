using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int CityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int CityId,
            int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task AddCityAsync(City city);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChancesAsync();
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);       
    }
}
