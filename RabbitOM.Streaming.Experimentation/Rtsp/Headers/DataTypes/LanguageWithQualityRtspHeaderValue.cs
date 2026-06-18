using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class LanguageWithQualityRtspHeaderValue
    {
        public LanguageWithQualityRtspHeaderValue( string language , string region )
        {
            throw new NotImplementedException();
        }

        public LanguageWithQualityRtspHeaderValue( string language , string region , double quality )
        {
            throw new NotImplementedException();
        }





        public string Language { get; }

        public string Region { get; }

        public double? Quality { get; }







        public static bool TryParse( string input , out LanguageWithQualityRtspHeaderValue result )
        {
            result = null;

            throw new NotImplementedException();
        }








        public override string ToString()
        {
            var fullName = $"{Language}-{Region}";

            return Quality.HasValue ? $"{fullName}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : fullName;
        }
    }
}
