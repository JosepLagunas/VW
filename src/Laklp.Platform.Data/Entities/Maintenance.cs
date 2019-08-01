using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Laklp.Platform.Data.Entities
{
    public class Maintenance : Traceable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Resource> DocumentaryResources { get; set; }
    }
}