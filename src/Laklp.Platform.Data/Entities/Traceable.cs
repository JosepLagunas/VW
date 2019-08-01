using System;

namespace Laklp.Platform.Data.Entities
{
    public abstract class Traceable
    {
        public User CreatedBy { get; set; }
        private const string ErrorMessageModifyCreationTime = "Cannot modify creation timeStamp.";

        private const string ErrorMessageUpdateModifyTime =
            "Cannot update modified timeStamp is not created timeStamp is previously set.";

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public void SetCreationTimeStamp()
        {
            if (Created != default(DateTime))
                throw new InvalidOperationException(ErrorMessageModifyCreationTime);

            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }

        protected void UpdateModificationTimeStamp()
        {
            if (Created == default(DateTime))
                throw new InvalidOperationException(ErrorMessageUpdateModifyTime);
            Modified = DateTime.UtcNow;
        }

        protected bool IsSetCreationTimeStamp()
        {
            return Created != default(DateTime);
        }
    }
}