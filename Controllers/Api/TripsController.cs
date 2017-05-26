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
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Trip> trips = _repository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(trips));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get all trips: {ex}");

                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                Trip newTrip = Mapper.Map<Trip>(trip);

                _repository.AddTrip(newTrip);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
            }

            return BadRequest("Failed to save the trip");
        }
    }
}
