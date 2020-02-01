using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cykelfest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var teamList = new List<Team>();
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("input.csv"))
                {
                    sr.ReadLine();
                    while(sr.Peek() >= 0)
                    {
                        String[] line = sr.ReadLine().Split(';');
                        teamList.Add(new Team
                        {
                            Name = line[0],
                            Address = line[1],
                            FoodPreferences = line[2],
                        });
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("kunde inte hitta input.csv");
                Console.WriteLine(e.Message);
            }


            Team[] teams = teamList.ToArray();
            //teams[0] = new Team("a");
            //teams[1] = new Team("b");
            //teams[2] = new Team("c");
            //teams[3] = new Team("d");
            //teams[4] = new Team("e");
            //teams[5] = new Team("f");
            //teams[6] = new Team("g");
            //teams[7] = new Team("h");
            //teams[8] = new Team("i");

            List<Group> groups = new DynamicProblemSolver().Solve(teams);

            // Skapa grupp fil
            var content = "FoodType;Host;Guests";
            foreach(var group in groups)
            {
                var _guests = string.Join(",", group.Guests.Select(g => g.Name));
                content += $"\n{group.FoodType};{group.Host.Name};{_guests}";
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("GruppFil.csv")))
            {
                outputFile.WriteLine(content);
            }

            // Skapa hostinfo per team
            content = "Team;FoodType";
            foreach (var group in groups)
            {
                content += $"\n{group.Host.Name};{group.FoodType}";
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("HostInfo.csv")))
            {
                outputFile.WriteLine(content);
            }

            // Skapa kvällsschema per team
            content = "Team;PreCourseHost;MainCourseHost;DessertHost";
            foreach (var team in teams)
            {
                var PreCourseGroup = team.Groups.Find(g => g.FoodType == FoodType.PreCourse);
                var MainCourseGroup = team.Groups.Find(g => g.FoodType == FoodType.MainCourse);
                var DessertGroup = team.Groups.Find(g => g.FoodType == FoodType.Dessert);
                content += $"\n{team.Name};{PreCourseGroup.Host.Name};{MainCourseGroup.Host.Name};{DessertGroup.Host.Name}";
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("KvällsSchema.csv")))
            {
                outputFile.WriteLine(content);
            }
        }
    }
}
