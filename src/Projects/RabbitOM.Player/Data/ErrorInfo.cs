using System;

namespace RabbitOM.Player.Data
{
    public sealed class ErrorInfo
    {
        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }


        public static implicit operator ErrorInfo( string message )
        {
            return new ErrorInfo() { TimeStamp = DateTime.Now , Message = message };
        }
    }
}
