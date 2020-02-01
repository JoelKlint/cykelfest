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
                groups[index].FoodType = FoodType.PreCourse;
                groups[index].Host = teams[indexFix(i* groupsPerCourse)];
                groups[index].Guests.Add(teams[indexFix(i* groupsPerCourse + 1)]);
                groups[index].Guests.Add(teams[indexFix(i* groupsPerCourse + 2)]);
           
            }

            // Create middle column
            for (var i = 0; i < groupsPerCourse; i++)
            {
                var index = i + groupsPerCourse;
                groups[index].FoodType = FoodType.MainCourse;
                groups[index].Host = teams[indexFix(i* groupsPerCourse + 1)];
                groups[index].Guests.Add(teams[indexFix(i* groupsPerCourse + 3)]);
                groups[index].Guests.Add(teams[indexFix(i* groupsPerCourse + 8)]);
            }

            // Create right column
            for (var i = 0; i < groupsPerCourse; i++)
            {
                var index = i + groupsPerCourse*2;
                groups[index].FoodType = FoodType.Dessert;
                groups[index].Host = teams[indexFix(i* groupsPerCourse + 2)];
                groups[index].Guests.Add(teams[indexFix(i* groupsPerCourse + 3)]);
                groups[index].Guests.Add(teams[indexFix(i* groupsPerCourse + 7)]);
            }
            return new List<Group>(groups);

        }

        private int indexFix(int index)
        {
            return index % teamCount;
        }
    }
}
