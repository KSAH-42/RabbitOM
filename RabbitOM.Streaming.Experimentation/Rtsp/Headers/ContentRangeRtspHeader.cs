using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentRangeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Range";
        




        private string _unit = string.Empty;
        private long? _from;
        private long? _to;
        private long? _size;
        



        public string Unit
        {
            get => _unit;
            set => _unit = StringRtspNormalizer.Normalize( value );
        }

        public long? From
        {
            get => _from;
            set => _from = value;
        }

        public long? To
        {
            get => _to;
            set => _to = value;
        }

        public long? Size
        {
            get => _size;
            set => _size = value;
        }





        public static bool TryParse( string input , out ContentRangeRtspHeader result )
        {
            result = null;

            // bytes 0-99/5000
            // bytes 0-99/*
            // bytes */5000

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , " " , out var tokens ) )
            {
                var unit = tokens.FirstOrDefault();

                if ( string.IsNullOrWhiteSpace( unit ) || unit.IndexOfAny( new char[] { ',' , ';' } ) >= 0 )
                {
                    return false;
                }

                var header = new ContentRangeRtspHeader() {  Unit = tokens.FirstOrDefault() };

                if ( RtspHeaderParser.TryParse( tokens.ElementAtOrDefault( 1 ) , "/" , out tokens ) || tokens.Length != 2 )
                {
                    if ( RtspHeaderParser.TryParse( tokens.ElementAtOrDefault( 0 ) , "-" , out var range ) || range.Length != 2 )
                    {
                        if ( long.TryParse( range.FirstOrDefault() , out var from ) )
                        {
                            header.From = from;
                        }

                        if ( long.TryParse( range.LastOrDefault() , out var to ) )
                        {
                            header.To = to;
                        }
                    }

                    if ( long.TryParse( tokens.LastOrDefault() , out var size ) )
                    {
                        header.Size = size;
                    }

                    if ( tokens.FirstOrDefault() != "*" && ( ! header.From.HasValue || ! header.To.HasValue ) )
                    {
                        return false;
                    }

                    if ( tokens.LastOrDefault() != "*" && ! header.Size.HasValue )
                    {
                        return false;
                    }
                }

                result = header;
            }

            return result != null;
        }
        




        public override bool TryValidate()
        {
            if ( From.HasValue && To.HasValue )
            {
                return StringRtspValidator.TryValidateAsContentTD( _unit );
            }

            return Size.HasValue && StringRtspValidator.TryValidateAsContentTD( _unit );
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _unit ) )
            {
                return string.Empty;
            }

            var buidler = new StringBuilder();

            buidler.AppendFormat( "{0} " , _unit );

            if ( From.HasValue && To.HasValue )
            {
                buidler.AppendFormat( "{0}-{1}" , From , To );
            }
            else
            {
                buidler.Append( "*" );
            }

            buidler.Append( "/" );

            if ( Size.HasValue )
            {
                buidler.AppendFormat( "{0}" , Size );
            }
            else
            {
                buidler.Append( "*" );
            }

            return buidler.ToString();

        }
    }
}
