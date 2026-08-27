using System;

namespace RabbitOM.Net.Sdp
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
