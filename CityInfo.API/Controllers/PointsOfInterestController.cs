using AutoMapper;
using CityInfo.API.Model;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ??
                throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(
            int cityId)
        {
            try
            {
                if (!await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    _logger.LogInformation(
                        $"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var pointsOfInterestForCity = await _cityInfoRepository
                    .GetPointsOfInterestForCityAsync(cityId);

                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting points of interest for city with id {cityId}.",
                    ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation(
                    $"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointsOfInterest = await _cityInfoRepository
                    .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointsOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointsOfInterest));
        }

        //[HttpPost]
        //public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        //    int cityId,
        //    PointOfInterestForCreatingDto pointOfInterest)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    // É falso quando alguma regra do model é quebrada.
        //    if (!ModelState.IsValid)
        //    {
        //
        //    }
        //
        //    // demo porpuses - to be improved
        //    var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
        //        c => c.PointsOfInterest).Max(p => p.Id);
        //
        //    var finalPointOfInterest = new PointOfInterestDto()
        //    {
        //        Id = ++maxPointOfInterestId,
        //        Name = pointOfInterest.Name,
        //        Description = pointOfInterest.Description
        //    };
        //
        //    city.PointsOfInterest.Add(finalPointOfInterest);
        //
        //    return CreatedAtRoute("GetPointOfInterest",
        //        new
        //        {
        //            cityId = cityId,
        //            pointOfInterestId = finalPointOfInterest.Id,
        //        },
        //        finalPointOfInterest);
        //}
        //
        //[HttpPut("{pointOfInterestId}")]
        //public ActionResult<PointOfInterestDto> UpdatePointOfInterest(
        //    int cityId,
        //    int pointOfInterestId,
        //    PointOfInterestForUpdateDto pointOfInterest)
        //{
        //    var city = _citiesDataStore.Cities
        //        .FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var pointOfInterestFromStore = city.PointsOfInterest
        //        .FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    pointOfInterestFromStore.Name = pointOfInterest.Name;
        //    pointOfInterestFromStore.Description = pointOfInterest.Description;
        //
        //    return NoContent();
        //}
        //
        //[HttpPatch("{pointOfInterestId}")]
        //public ActionResult<PointOfInterestDto> PartiallyUpdatePointOfInterest(
        //    int cityId, int pointOfInterestId,
        //    JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{
        //    var city = _citiesDataStore.Cities
        //        .FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var pointOfInterestFromStore = city.PointsOfInterest
        //        .FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var pointOfInterestToPatch =
        //        new PointOfInterestForUpdateDto()
        //        {
        //            Name = pointOfInterestFromStore.Name,
        //            Description = pointOfInterestFromStore.Description
        //        };
        //
        //    patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
        //
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //
        //    if (!TryValidateModel(pointOfInterestToPatch))
        //    {
        //        return BadRequest(ModelState);
        //    }
        //
        //    pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        //    pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        //
        //    return NoContent();
        //}
        //
        //[HttpDelete("{pointOfInterestId}")]
        //public ActionResult<PointOfInterestDto> DeletePointOfInterest(
        //    int cityId,
        //    int pointOfInterestId)
        //{
        //    var city = _citiesDataStore.Cities
        //        .FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var pointOfInterestFromStore = city.PointsOfInterest
        //        .FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    city.PointsOfInterest.Remove(pointOfInterestFromStore);
        //    _mailService.Send(
        //        "Point of interest deleted.",
        //        $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted");
        //    return NoContent();
        //}
    }
}
