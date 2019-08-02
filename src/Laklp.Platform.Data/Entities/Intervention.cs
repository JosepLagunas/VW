using System;
using System.Collections.Generic;
using Laklp.Platform.Data.Entities.Enums;

namespace Laklp.Platform.Data.Entities
{
    public class Intervention : Traceable, ILocalizable
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public Geocoordinate Geocoordinate { get; set; }
        public EvaluationStatus EvaluationStatus { get; set; }
        public EvaluationResults EvaluationResult { get; set; }
        public ICollection<CheckPoint> CheckPoints { get; set; }
        public Delegation Delegation { get; set; }
        public MaintenanceService MaintenanceService { get; set; }
        public Employee AssignedTo { get; set; }
        public string InterventionDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Entity Entity { get; set; }
    }
}