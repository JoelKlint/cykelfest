using System;
using System.Collections.Generic;

namespace cykelfest
{
    public class Team
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string FoodPreferences { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
