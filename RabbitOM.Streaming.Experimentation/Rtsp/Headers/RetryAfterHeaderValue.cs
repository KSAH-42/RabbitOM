using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Extensions;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;
   
    public sealed class RetryAfterHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        



        private readonly DateTime? _date;
        private readonly TimeSpan? _delta;
        



        public RetryAfterHeaderValue( DateTime date )
        {
            _date = date;
        }

        public RetryAfterHeaderValue( TimeSpan delta )
        {
            _delta = delta;
        }
        

        public DateTime? Date
        {
            get => _date;
        }

        public TimeSpan? Delta
        {
            get => _delta;
        }




        public static bool TryParse( string input , out RetryAfterHeaderValue result )
        {
            result = null;

            var value = ValueNormalizer.Normalize( input );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( DateTime.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var date ) )
            {
                result = new RetryAfterHeaderValue( date );
            }
            else if ( ulong.TryParse( value , out var seconds ) )
            {
                result = new RetryAfterHeaderValue( TimeSpan.FromSeconds( seconds ) );
            }
                
            return result != null;
        }




        public override string ToString()
        {
            if ( _date.HasValue )
            {
                return _date.Value.ToGmtDate();
            }

            if ( _delta.HasValue )
            {
                return _delta.Value.TotalSeconds.ToString();
            }

            return string.Empty;
        }
    }
}
