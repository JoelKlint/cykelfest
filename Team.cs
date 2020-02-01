using System;
using System.Collections.Generic;

namespace cykelfest
{
    public class Team
    {
        public Team(string _name)
        {
            Name = _name;
        }
        public string Name { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
