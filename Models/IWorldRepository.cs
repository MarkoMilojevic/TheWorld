using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrip(Trip trip);

        Task<bool> SaveChangesAsync();

        Trip GetTripByName(string tripName);

        void AddStop(string tripName, Stop stop);
    }
}
