// RabbitOM.Player.exe setup rtsp://what.is.a.git.from.urban.dictionary?q=definition
// RabbitOM.Player.exe setup rtsp://example.burnelion@figona.is.a.just.the.true.git?comment=from_him_gc_works_also_on_unmanaged_heap_it_calls_cpp_destructors_and_delete_pointers_and_put_msdn_writes_into_the_garbage --stretch "this poor engineer"
using System;

namespace RabbitOM.Player.Cli
{
    [Command( "setup" )]
    public sealed class SetupCommand : Command
    {
        private readonly IApplication _application;


        public SetupCommand( IApplication application )
        {
            _application = application ?? throw new ArgumentNullException( nameof( application ) );
        }


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
            _application.StreamUri = Uri;

            if ( ShowStats )
            {
                _application.ShowStatistics();
            }
            else
            {
                _application.HideStatistics();
            }

            if ( StrechImage )
            {
                _application.StrechImage();
            }
            else
            {
                _application.UnStrechImage();
            }

            if ( AutoStart )
            {
                _application.StartStreaming();
            }
        }
    }
}
