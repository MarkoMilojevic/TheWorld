using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace WebApp.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}
