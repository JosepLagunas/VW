using System;

namespace Laklp.Platform.Data.Entities
{
    public abstract class Traceable
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}