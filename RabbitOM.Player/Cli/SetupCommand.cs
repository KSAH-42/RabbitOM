// RabbitOM.Player.exe setup rtsp://abcd.youtube.com/watch?v=eJnQBXmZ7Ek&list=RDcUxHR3XWjIo&index=15
// RabbitOM.Player.exe setup rtsp://abcd --play --no-stats --no-stretch
using System;

namespace RabbitOM.Player.Cli
{
    [Command( "setup" )]
    public sealed class SetupCommand : Command , ICommandHandler<SetupCommand>
    {
        private Action<SetupCommand> _handler;


        [OptionPosition(0)]
        public string Uri { get; set; }

        [Option("--play" , true )]
        [Option("--no-play" , false )]
        public bool AutoStart { get; set; }

        [Option("--stats" , true )]
        [Option("--no-stats" , false )]
        public bool ShowStats { get; set; }

        [Option("--stretch" , true )]
        [Option("--no-stretch" , false)]
        public bool StrechImage { get; set; }


        public override void Execute()
        {
            var handler = _handler ?? throw new InvalidOperationException( "no handler has been defined" );

            handler.Invoke( this );
        }

        public void SetHandler( Action<SetupCommand> handler )
        {
            _handler = handler ?? throw new ArgumentNullException( nameof( handler ) );
        }
    }
}
