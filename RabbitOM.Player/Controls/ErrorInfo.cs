using System;

namespace RabbitOM.Player.Controls
{
    public sealed class ErrorInfo
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public string Message { get; set; }
    }
}
