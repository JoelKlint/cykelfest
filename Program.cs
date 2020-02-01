using System;
using System.Collections.Generic;

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

            List<Group> solution = new ProblemSolver().Solve(teams);

            foreach(var group in solution)
            {
                System.Console.WriteLine(group);
            }

        }
    }
}
