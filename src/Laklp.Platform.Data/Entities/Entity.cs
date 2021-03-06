using System;
using System.Collections.Generic;

namespace Laklp.Platform.Data.Entities
{
    public class Entity : Traceable, ILocalizable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Entity> Components { get; set; }
        public Entity Parent { get; set; }
        public ICollection<DocumentaryResource> DocumentaryResources { get; set; }
        public string Address { get; set; }
        public Geocoordinate Geocoordinate { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
    }
}