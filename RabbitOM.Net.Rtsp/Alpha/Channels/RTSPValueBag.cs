using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPValueBag<TValue>
    {
        private readonly object _lock = new object();

        private TValue _value;

        public TValue Value
        {
            get
            {
                lock ( _lock )
                {
                    return _value;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _value = value;
                }
            }
        }

    }
}
