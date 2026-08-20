using System;

namespace RabbitOM.Player.Cli
{
    public sealed class ApplicationCommand : Command , ICommandHandler<ApplicationCommand>
    {
        private Action<ApplicationCommand> _handler;

        [CommandOption("-s")]
        [CommandOption("--show-stats")]
        public bool ShowStats { get; set; }

        [CommandOption("-i")]
        [CommandOption("--stretch-image")]
        public bool StrechImage { get; set; }

        [CommandOption("-d")]
        [CommandOption("--start-streaming")]
        public bool StartStreaming { get; set; }

        [CommandOption("-u")]
        [CommandOption("--uri")]
        public string Uri { get; set; }




        public override void Execute()
        {
            if ( _handler == null )
            {
                throw new InvalidOperationException( "no handler has been defined" );
            }

            _handler( this );
        }

        public void SetHandler( Action<ApplicationCommand> handler )
        {
            _handler = handler ?? throw new ArgumentNullException( nameof( handler ) );
        }
    }
}
