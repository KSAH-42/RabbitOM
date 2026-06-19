using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderValueCharSet
    {
        private const string DefaultLettersLowerCase = "azertyuiopqsdfghjklmwxcvbn";
        private const string DefaultLettersUpperCase = "AZERTYUIOPQSDFGHJKLMWXCVBN";
        private const string DefaultDigits = "0123456789";
        private const string DefaultSymbols1 = "!#$%&'*+-.^_|~";
        private const string DefaultSymbols2 = "/\\ {}[]()<>\"'`";

        public RtspHeaderValueCharSet( IReadOnlyCollection<char> values )
        {
            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( values.Count <= 0 )
            {
                throw new ArgumentException( nameof( values ) );
            }

            Values = values;
        }

        public IReadOnlyCollection<char> Values { get; }

        public static RtspHeaderValueCharSet Letters { get; } = new RtspHeaderValueCharSet( (DefaultLettersUpperCase + DefaultLettersLowerCase).ToHashSet<char>() );

        public static RtspHeaderValueCharSet Digits { get; } = new RtspHeaderValueCharSet( DefaultDigits.ToHashSet<char>() );

        public static RtspHeaderValueCharSet Symbols1 { get; } = new RtspHeaderValueCharSet( DefaultSymbols1.ToHashSet<char>() );

        public static RtspHeaderValueCharSet Symbols2 { get; } = new RtspHeaderValueCharSet( DefaultSymbols2.ToHashSet<char>() );

        public static RtspHeaderValueCharSet BasicToken { get; } = new RtspHeaderValueCharSet( (DefaultLettersLowerCase + DefaultLettersUpperCase + DefaultDigits + DefaultSymbols1 ).ToHashSet<char>() );
    }
}
