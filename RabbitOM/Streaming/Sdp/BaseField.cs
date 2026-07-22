using System;

namespace RabbitOM.Streaming.Sdp
{
    public abstract class BaseField
    {
        public abstract string TypeName
        {
            get;
        }

        public virtual void Validate()
        {
            if ( ! TryValidate() )
            {
                throw new ValidationException();
            }
        }

        public abstract bool TryValidate();

        public abstract override string ToString();
    }
}
