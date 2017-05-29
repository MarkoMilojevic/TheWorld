using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApp.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all trips from the Database");
            
            return _context.Trips.ToList();
        }

        public IEnumerable<Trip> GetTripsByUsername(string username)
        {
            return _context
                    .Trips
                    .Include(t => t.Stops)
                    .Where(t => t.UserName == username)
                    .ToList();
        }

        public void AddTrip(Trip trip)
        {
            _context.Trips.Add(trip);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            return _context
                .Trips
                .Include(t => t.Stops)
                .FirstOrDefault(t => t.Name == tripName);
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            return _context
                .Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName && t.UserName == username)
                .FirstOrDefault(t => t.Name == tripName);
        }

        public void AddStop(string tripName, Stop stop, string username)
        {
            Trip trip = GetUserTripByName(tripName, username);
            if (trip == null)
            {
                return;
            }

            trip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }
    }
}
