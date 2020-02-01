using System;
using System.Collections.Generic;
using System.Linq;

namespace cykelfest
{
    public class Group
    {
        public FoodType FoodType { get; set; }
        public Team Host { get; set; }
        public List<Team> Guests { get; set; } = new List<Team>();

        public override string ToString()
        {
            var _guests = string.Join(",", Guests.Select(g => g.Name));
            return $"Type: {FoodType}.\t\tHost: {Host.Name}.\tGuests: {_guests}";
        }
    }

}
