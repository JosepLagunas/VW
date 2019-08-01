using System;
using System.Collections.Generic;

namespace Laklp.Platform.Data.Entities
{
    public class Company : Traceable, ILocalizable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyLegalNumber { get; set; }
        public string Address { get; set; }
        public Geocoordinate Geocoordinate { get; set; }
        public ICollection<Delegation> Delegations { get; set; }
        public ICollection<DocumentaryResource> DocumentaryResources { get; set; }
    }
}