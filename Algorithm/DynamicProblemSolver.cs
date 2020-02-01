using System;
using System.Collections.Generic;

namespace cykelfest
{
    public class DynamicProblemSolver : IProblemSolver
    {
        public List<Group> Solve(Team[] teams)
        {

            var groupList = new List<Group>();

            int teamsPerGroup = 3; // Should be possible to change, but not to a higher value than colCount

            int courseCount = 3; // AKA amount of courses
            int rowCount = teams.Length / courseCount; // groups per course / groups eating at the same time

            Group[,] groups = new Group[courseCount, rowCount];

            for(var i = 0; i < teams.Length; i++)
            {
                // Figure out indexes
                int firstRowIndex = i / 3;
                int firstGroupIndex = i % 3;
                int secondRowIndex = -1;
                int secondGroupIndex = -1;
                int thirdRowIndex = -1;
                int thirdGroupIndex = -1;
                switch (firstGroupIndex)
                {
                    case 0:
                        secondRowIndex = firstRowIndex;
                        thirdRowIndex = firstRowIndex;
                        secondGroupIndex = 1;
                        thirdGroupIndex = 2;
                        break;
                    case 1:
                        secondRowIndex = (firstRowIndex + 1) % rowCount;
                        thirdRowIndex = (firstRowIndex + 2) % rowCount;
                        secondGroupIndex = 0;
                        thirdGroupIndex = 2;
                        break;
                    case 2:
                        secondRowIndex = (firstRowIndex + 2) % rowCount;
                        thirdRowIndex = (firstRowIndex + 1) % rowCount;
                        secondGroupIndex = 1;
                        thirdGroupIndex = 0;
                        break;
                    default:
                        throw new Exception("You got a problem yo");
                }


                // Create first group if not exist
                if (groups[0,firstRowIndex] == null)
                {
                    var newGroup = new Group();
                    newGroup.FoodType = FoodType.PreCourse;
                    groups[0, firstRowIndex] = newGroup;
                    groupList.Add(newGroup);
                }
                // Create second group if not exist
                if (groups[1, secondRowIndex] == null)
                {
                    var newGroup = new Group();
                    groups[1, secondRowIndex] = newGroup;
                    newGroup.FoodType = FoodType.MainCourse;
                    groupList.Add(newGroup);
                }
                // Create third group if not exist
                if (groups[2, thirdRowIndex] == null)
                {
                    var newGroup = new Group();
                    groups[2, thirdRowIndex] = newGroup;
                    newGroup.FoodType = FoodType.Dessert;
                    groupList.Add(newGroup);
                }

                // Place into first column
                if (firstGroupIndex == 0) // is host
                    groups[0, firstRowIndex].Host = teams[i];
                else
                    groups[0, firstRowIndex].Guests.Add(teams[i]);
                teams[i].Groups.Add(groups[0, firstRowIndex]);

                // Place into second column
                if (secondGroupIndex == 0) // is host
                    groups[1, secondRowIndex].Host = teams[i];
                else
                    groups[1, secondRowIndex].Guests.Add(teams[i]);
                teams[i].Groups.Add(groups[1, secondRowIndex]);

                // Place into third column
                if (thirdGroupIndex == 0) // is host
                    groups[2, thirdRowIndex].Host = teams[i];
                else
                    groups[2, thirdRowIndex].Guests.Add(teams[i]);
                teams[i].Groups.Add(groups[2, thirdRowIndex]);

            }

            return groupList;
        }
    }
}
