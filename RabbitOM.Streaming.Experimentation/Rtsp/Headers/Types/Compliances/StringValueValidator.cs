using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public abstract class StringValueValidator : IValidator<string>
    {
        protected static readonly IReadOnlyCollection<char> InvalidChars = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  , '[' , ']' , '{' , '}' , '<' , '>' };

        

        
        // TODO: refactor using regular expression and inject patterns on the config class for specific type

        public static StringValueValidator DefaultValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() );
        
        public static StringValueValidator AcceptEmptyOrWhiteSpaceValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() { AcceptEmptyOrWhiteSpace = true } );
        
        public static StringValueValidator LetterOrDigitValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() { OnValidate = value => value.Any( Char.IsLetterOrDigit ) } );
        
        public static StringValueValidator TextWithNoSpaceValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() { OnValidate = value => value.Contains( ' ' ) == false } );
        
        public static StringValueValidator LetterOrDigitOrStarValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() { OnValidate = value => value.All( character => Char.IsLetterOrDigit(character) || character == '*' ) }  );
        
        public static StringValueValidator CommentValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() { OnValidate = value => ! value.Contains( '(' ) && ! value.Contains( ')' ) } );
        
        public static StringValueValidator MimeValidator { get; } = new DefaultHeaderValidator( new DefaultHeaderValidatorSettings() { OnValidate = value => value.IndexOf( '/' ) > 0 && value.Count( x => x == '/' ) == 1 && value.Any( x => x == '\\' || x == ' ' ) == false } );

        public static StringValueValidator UriValidator { get; } = new UriStringValidator();
        




        public abstract bool TryValidate( string value );
    }
}
