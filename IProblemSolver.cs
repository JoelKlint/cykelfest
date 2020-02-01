using System;
using System.Collections.Generic;

namespace cykelfest
{
    public interface IProblemSolver
    {
        List<Group> Solve(Team[] teams);
    }
}
