using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<TripsController> _logger;
        private readonly GeoCoordsService _coordsService;

        public StopsController(IWorldRepository repository, ILogger<TripsController> logger, GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet]
        public IActionResult Get(string tripName)
        {
            try
            {
                Trip trip = _repository.GetTripByName(tripName);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops).OrderBy(s => s.Order));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}: {ex}");

                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel stop)
        {
            if (ModelState.IsValid)
            {
                Stop newStop = Mapper.Map<Stop>(stop);

                GeoCoordsResult result = await _coordsService.GetCoordsAsync(newStop.Name);
                if (!result.Success)
                {
                    _logger.LogError(result.Message);
                }
                else
                {
                    newStop.Latitude = result.Latitude;
                    newStop.Longitude = result.Longitude;

                    _repository.AddStop(tripName, newStop);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }

            return BadRequest("Failed to save the stop");
        }
    }
}
