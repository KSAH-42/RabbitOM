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
            return new StringParameterRtspHeaderValue( RtspHeaderValueValidator.EnsureWellFormed( name , RtspHeaderValueCharSet.BasicToken  ) , RtspHeaderValueValidator.EnsureWellFormed( value , RtspHeaderValueCharSet.BasicToken ) );
        }

        public static bool TryCreate( string name , string value , out StringParameterRtspHeaderValue result )
        {
            result = RtspHeaderValueValidator.IsWellFormed( name  , RtspHeaderValueCharSet.BasicToken )
                  && RtspHeaderValueValidator.IsWellFormed( value , RtspHeaderValueCharSet.BasicToken )
                  ? new StringParameterRtspHeaderValue( name , value )
                  : null;

            return result != null;
        }
    }
}
