using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderValueValidatorCharSet
    {
        private const string DefaultLetters = "azertyuiopqsdfghjklmwxcvbn" + "AZERTYUIOPQSDFGHJKLMWXCVBN";
        private const string DefaultDigits = "0123456789";
        private const string DefaultSymbols1 = "!#$%&'*+-.^_|~";
        private const string DefaultSymbols2 = "/\\ {}[]()<>\"'`";

        private RtspHeaderValueValidatorCharSet( IReadOnlyCollection<char> values ) => Values = values;

        public IReadOnlyCollection<char> Values { get; }

        public static RtspHeaderValueValidatorCharSet Symbols1 { get; } = new RtspHeaderValueValidatorCharSet( DefaultSymbols1.ToHashSet<char>() );

        public static RtspHeaderValueValidatorCharSet Symbols2 { get; } = new RtspHeaderValueValidatorCharSet( DefaultSymbols2.ToHashSet<char>() );

        public static RtspHeaderValueValidatorCharSet Digits { get; } = new RtspHeaderValueValidatorCharSet( DefaultDigits.ToHashSet<char>() );

        public static RtspHeaderValueValidatorCharSet Letters { get; } = new RtspHeaderValueValidatorCharSet( DefaultLetters.ToHashSet<char>() );

        public static RtspHeaderValueValidatorCharSet BasicToken { get; } = new RtspHeaderValueValidatorCharSet( (DefaultLetters + DefaultDigits + DefaultSymbols1).ToHashSet<char>() );
    }
}
