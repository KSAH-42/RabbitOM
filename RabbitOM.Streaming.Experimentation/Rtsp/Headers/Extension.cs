using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class Extension
    {
        public static readonly Extension Empty = new Extension( string.Empty, string.Empty );




        public Extension( string name , string value )
        {
            Name = name ?? string.Empty;
            Value = value ?? string.Empty;
        }



        public string Name { get; }

        public string Value { get; }




        public bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( Name );
        }
    }
}
