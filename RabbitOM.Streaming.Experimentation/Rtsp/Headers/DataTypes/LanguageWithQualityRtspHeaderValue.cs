using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class LanguageWithQualityRtspHeaderValue
    {
        private const string Pattern = "^[A-Za-z]{1,8}(-[A-Za-z0-9]{1,8})*$";




        public LanguageWithQualityRtspHeaderValue( string name , string region )
            : this ( name , region , null )
        {
        }

        public LanguageWithQualityRtspHeaderValue( string name , string region , double quality )
            : this ( name , region , (double?) quality )
        {
        }

        private LanguageWithQualityRtspHeaderValue( string name , string region , double? quality )
        {
            // RtspHeaderValueValidator.EnsureToken( )
            // RtspHeaderValueValidator.EnsureToken( )
            // RtspHeaderValueValidator.EnsureEquals( excepted , value );
            
            // FullName = $"{Name}-{Region}";
            throw new NotImplementedException();
        }





        public string FullName { get; }

        public string Name { get; }

        public string Region { get; }

        public double? Quality { get; }






        public static bool TryParse( string input , out LanguageWithQualityRtspHeaderValue result )
        {
            throw new NotImplementedException();
        }






        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
