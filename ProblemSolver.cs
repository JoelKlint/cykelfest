using System;
using System.Collections.Generic;

namespace cykelfest
{
    public class ProblemSolver : IProblemSolver
    {
        private int teamCount;
        public List<Group> Solve(Team[] teams)
        {
            teamCount = teams.Length;
            // Vi utgår från at teams är jämnt delbart med 3!!!
            Group[] groups = new Group[teams.Length];
            for(var i = 0; i<groups.Length; i++)
            {
                groups[i] = new Group();
            }

            var courseAmount = 3;
            var groupsPerCourse = teams.Length / courseAmount;
            // TODO: Instanstiate groups

            // Create left most column 
            for (var i = 0; i < groupsPerCourse; i++)
            {
                var index = i;

                var group = groups[index];
                var teamX = teams[indexFix(i * groupsPerCourse)];
                var teamY = teams[indexFix(i * groupsPerCourse + 1)];
                var teamZ = teams[indexFix(i * groupsPerCourse + 2)];

                group.FoodType = FoodType.PreCourse;

                group.Host = teamX;
                group.Guests.Add(teamY);
                group.Guests.Add(teamZ);

                teamX.Groups.Add(group);
                teamY.Groups.Add(group);
                teamZ.Groups.Add(group);

            }

            // Create middle column
            for (var i = 0; i < groupsPerCourse; i++)
            {
                var index = i + groupsPerCourse;

                var group = groups[index];
                var teamX = teams[indexFix(i * groupsPerCourse + 1)];
                var teamY = teams[indexFix(i * groupsPerCourse + 3)];
                var teamZ = teams[indexFix(i * groupsPerCourse + 8)];

                groups[index].FoodType = FoodType.MainCourse;

                group.Host = teamX;
                group.Guests.Add(teamY);
                group.Guests.Add(teamZ);

                teamX.Groups.Add(group);
                teamY.Groups.Add(group);
                teamZ.Groups.Add(group);
            }

            // Create right column
            for (var i = 0; i < groupsPerCourse; i++)
            {
                var index = i + groupsPerCourse*2;

                var group = groups[index];
                var teamX = teams[indexFix(i * groupsPerCourse + 2)];
                var teamY = teams[indexFix(i * groupsPerCourse + 3)];
                var teamZ = teams[indexFix(i * groupsPerCourse + 7)];

                groups[index].FoodType = FoodType.Dessert;

                group.Host = teamX;
                group.Guests.Add(teamY);
                group.Guests.Add(teamZ);

                teamX.Groups.Add(group);
                teamY.Groups.Add(group);
                teamZ.Groups.Add(group);
            }
            return new List<Group>(groups);

        }

        private int indexFix(int index)
        {
            return index % teamCount;
        }
    }
}
