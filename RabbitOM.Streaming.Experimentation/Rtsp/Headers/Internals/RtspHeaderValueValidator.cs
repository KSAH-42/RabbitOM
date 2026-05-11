using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValueValidator
    {
        public static string EnsureWellFormedToken( string value )
        {
            throw new NotImplementedException();
        }

        public static string EnsureWellFormedTokenOrEmpty( string value )
        {
            throw new NotImplementedException();
        }

        public static string EnsureNotNullOrEmpty( string  value )
        {
            throw new NotImplementedException();
        }

        public static string EnsureContainsNoSpace( string  value )
        {
            throw new NotImplementedException();
        }

        public static string EnsureNotNullOrWhiteSpace( string value )
        {
            throw new NotImplementedException();
        }

        public static string EnsureHasLettersAndDigits( string  value )
        {
            throw new NotImplementedException();
        }

        public static string EnsureContains( string value , Func<char, bool> predicate )
        {
            throw new NotImplementedException();
        }

        public static string EnsureNoQuotes( string value )
        {
            throw new NotImplementedException();
        }


        public static bool TryEnsureNotNullOrEmpty( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryEnsureNoSpace( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryEnsureWellFormedToken( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryEnsureWellFormedTokenOrEmpty( string value )
        {
            throw new NotImplementedException();
        }

        public static bool Contains( string value , Func<char , bool> predicate )
        {
            throw new NotImplementedException();
        }

        public static bool ContainsNoSpace( string value )
        {
            throw new NotImplementedException();
        }
    }
}
