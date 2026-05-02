using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public abstract class StringValueChecker : IValueChecker<string>
    {
        public static StringValueChecker PropertyChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker StringWithQualityChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker ParseChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker ExtensionChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker ParameterNameChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker ParameterValueChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker LanguageChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker EncodingChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker TransportChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker TransmissionChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker MimeChecker { get; } = new DefaultStringValueChecker();

        public static StringValueChecker HeaderNameChecker { get; } = new DefaultStringValueChecker();





        
        public abstract bool CheckValue( string value );

        public abstract string EnsureValue( string value );
    }

    /*
     
    public abstract class StringWithQualityValueChecker : IValueChecker<StringWithQuality>
    {
    }
     */
}
