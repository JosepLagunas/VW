using System;
using Laklp.Platform.Data.Entities.Enums;

namespace Laklp.Platform.Data.Entities
{
    public class CheckPoint : Traceable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ChallengeQuestion { get; set; }
        public CheckPointChallengeType ChallengeType { get; set; }
        public bool Satisfied { get; set; }
        public CheckPointStatus CheckPointStatus { get; set; }
        public Intervention Intervention { get; set; }
    }
}