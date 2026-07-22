using System;

namespace RabbitOM.Streaming.Sdp
{
    public abstract class AttributeValue
    {
        public virtual void Validate()
        {
            if ( ! TryValidate() )
            {
                throw new ValidationException();
            }
        }

        public abstract bool TryValidate();
    }
}
