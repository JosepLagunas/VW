using System;
using System.Collections.Generic;
using Laklp.Platform.Data.Entities.Enums;

namespace Laklp.Platform.Data.Entities
{
    public class Delegation : Traceable, ILocalizable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public DelegationType DelegationType { get; set; }
        public string Address { get; set; }
        public Geocoordinate Geocoordinate { get; set; }
        public ICollection<MaintenanceService> MaintenanceServices { get; set; }
    }
}