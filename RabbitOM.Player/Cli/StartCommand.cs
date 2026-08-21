using System;

namespace RabbitOM.Player.Cli
{
    [Command( "start" ) ]
    public sealed class StartCommand : Command , ICommandHandler<StartCommand>
    {
        private Action<StartCommand> _handler;


        [CommandOption(0)]
        [CommandOption("-u")]
        [CommandOption("--uri")]
        public string Uri { get; set; }

        [CommandOption("-s")]
        [CommandOption("--show")]
        public bool Show { get; set; } = true;

        [CommandOption("--show-stats")]
        public bool ShowStats { get; set; } = true;

        [CommandOption("-i")]
        [CommandOption("--stretch-image")]
        public bool StrechImage { get; set; } = true;


        public override void Execute()
        {
            if ( _handler == null )
            {
                throw new InvalidOperationException( "no handler has been defined" );
            }

            _handler( this );
        }

        public void SetHandler( Action<StartCommand> handler )
        {
            _handler = handler ?? throw new ArgumentNullException( nameof( handler ) );
        }
    }
}
