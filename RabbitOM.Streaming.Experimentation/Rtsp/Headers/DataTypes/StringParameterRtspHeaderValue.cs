using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class StringParameterRtspHeaderValue
    {
        private StringParameterRtspHeaderValue( string name , string value )
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }


        public static StringParameterRtspHeaderValue Create( string name , string value )
        {
            return new StringParameterRtspHeaderValue( RtspHeaderValueValidator.EnsureWellFormedToken( name ) , RtspHeaderValueValidator.EnsureWellFormedToken( value ) );
        }

        public static bool TryCreate( string name , string value , out StringParameterRtspHeaderValue result )
        {
            result = RtspHeaderValueValidator.TryEnsureWellFormedToken( name )
                  && RtspHeaderValueValidator.TryEnsureWellFormedToken( value )
                  ? new StringParameterRtspHeaderValue( name , value )
                  : null;

            return result != null;
        }
    }
}
