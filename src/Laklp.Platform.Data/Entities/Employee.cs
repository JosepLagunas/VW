using System;
using System.Collections.Generic;

namespace Laklp.Platform.Data.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Company WorksFor { get; set; }
        public Delegation WorksAt { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
        public TeamLead Responsible { get; set; } 
        public User User { get; set; }
    }
}