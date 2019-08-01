using System;
using System.Collections.Generic;

namespace Laklp.Platform.Data.Entities
{
    public class Entity : Traceable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Entity> Components { get; set; }
        public Entity Parent { get; set; }
        public ICollection<Resource> DocumentaryResources { get; set; }
    }
}