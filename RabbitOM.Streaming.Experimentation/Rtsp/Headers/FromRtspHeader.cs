using System;
using System.Net.Mail;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class FromRtspHeader : RtspHeader 
    {
        public const string TypeName = "From";
        




        private string _address;
        




        public string Address
        {
            get => _address ;
            set => _address = value;
        }




        public override bool TryValidate()
        {
            return _address != null;
        }

        public override string ToString()
        {
            return _address?.ToString() ?? string.Empty;
        }
        





        public static FromRtspHeader Parse( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return TryParse( value , out var result ) ? result : throw new FormatException();
        }

        public static bool TryParse( string value , out FromRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
