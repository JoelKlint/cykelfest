using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace cykelfest
{
    class Solver
    {
        public string CreateOutputFile(string filepath)
        {
            var teamList = new List<Team>();
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filepath))
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
                Console.WriteLine($"kunde inte hitta {filepath}");
                Console.WriteLine(e.Message);
            }


            Team[] teams = teamList.ToArray();

            // Shuffle list
            var rand = new Random();
            teams = teams.OrderBy(t => rand.NextDouble()).ToArray();

            List<Group> groups = new DynamicProblemSolver().Solve(teams);

            var workDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "result");
            Directory.CreateDirectory(workDir);

            // Skapa grupp fil
            var content = "FoodType;Host;Guests";
            foreach(var group in groups)
            {
                var _guests = string.Join(",", group.Guests.Select(g => g.Name));
                content += $"\n{group.FoodType};{group.Host.Name};{_guests}";
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(workDir, "GruppFil.csv")))
            {
                outputFile.WriteLine(content);
            }

            // Skapa hostinfo per team
            content = "Team;FoodType";
            foreach (var group in groups)
            {
                content += $"\n{group.Host.Name};{group.FoodType}";
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(workDir, "HostInfo.csv")))
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
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(workDir, "KvällsSchema.csv")))
            {
                outputFile.WriteLine(content);
            }

            var zipFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            ZipFile.CreateFromDirectory(workDir, zipFilePath);

            return zipFilePath;
        }
    }
}
