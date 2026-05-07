using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public abstract class StringValueChecker : IValueChecker<string>
    {
        protected static readonly IReadOnlyCollection<char> InvalidChars = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  , '[' , ']' , '{' , '}' , '<' , '>' };


        public static StringValueChecker Default { get; } = new DefaultStringValueChecker();

        public static StringValueChecker StringQualityChecker { get; } = Default;



        public abstract bool CheckValue( string value );

        public abstract string EnsureValue( string value );
    }

}
