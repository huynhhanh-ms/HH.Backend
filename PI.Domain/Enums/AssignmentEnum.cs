using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Enums
{
    public enum AssignmentPiority
    {
        Highest,
        High,
        Medium,
        Low,
        Lowest
    }

    public enum AssignmentStatus
    {
        Todo,
        InProgress,
        Done
    }

    public enum AssignmentRole
    {
        Assignee,
        Reporter
    }
}
