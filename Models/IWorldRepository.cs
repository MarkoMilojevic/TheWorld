using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        
        IEnumerable<Trip> GetTripsByUsername(string username);

        void AddTrip(Trip trip);

        Task<bool> SaveChangesAsync();

        Trip GetTripByName(string tripName);

        Trip GetUserTripByName(string tripName, string username);

        void AddStop(string tripName, Stop stop, string username);
    }
}
