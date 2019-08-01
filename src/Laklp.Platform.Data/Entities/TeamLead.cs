using System.Collections.Generic;

namespace Laklp.Platform.Data.Entities
{
    public class TeamLead : Employee
    {
        private ICollection<Employee> ResponsibleOf { get; set; }
    }
}