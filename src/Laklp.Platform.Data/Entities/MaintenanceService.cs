using System;
using System.Collections;
using System.Collections.Generic;

namespace Laklp.Platform.Data.Entities
{
    public class MaintenanceService : Traceable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DocumentaryResource> DocumentaryResources { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
    }
}