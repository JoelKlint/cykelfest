using System;
using System.Collections.Generic;
using System.Linq;

namespace cykelfest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Team[] teams = new Team[9];
            teams[0] = new Team("a");
            teams[1] = new Team("b");
            teams[2] = new Team("c");
            teams[3] = new Team("d");
            teams[4] = new Team("e");
            teams[5] = new Team("f");
            teams[6] = new Team("g");
            teams[7] = new Team("h");
            teams[8] = new Team("i");

            List<Group> groups = new ProblemSolver().Solve(teams);

            foreach(var group in groups)
            {
                System.Console.WriteLine(group);
            }

            // Skapa grupp fil
            var content = "FoodType;Host;Guests";
            foreach(var group in groups)
            {
                var _guests = string.Join(",", group.Guests.Select(g => g.Name));
                content += $"\n{group.FoodType};{group.Host.Name};{_guests}";
            }
            //Console.WriteLine(content);

            // Skapa hostinfo per team
            content = "Team;FoodType";
            foreach (var group in groups)
            {
                content += $"\n{group.Host.Name};{group.FoodType}";
            }
            Console.WriteLine(content);

            // Skapa kvällsschema per team
            content = "Team;PreCourseHost;MainCourseHost;DessertHost";
            foreach (var team in teams)
            {
                var PreCourseGroup = team.Groups.Find(g => g.FoodType == FoodType.PreCourse);
                var MainCourseGroup = team.Groups.Find(g => g.FoodType == FoodType.MainCourse);
                var DessertGroup = team.Groups.Find(g => g.FoodType == FoodType.Dessert);
                content += $"\n{team.Name};{PreCourseGroup.Host.Name};{MainCourseGroup.Host.Name};{DessertGroup.Host.Name}";
            }
            Console.WriteLine(content);
        }
    }
}
