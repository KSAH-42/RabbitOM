// RabbitOM.Player.exe setup rtsp://what.is.a.git.from.urban.dictionary?q=definition
// RabbitOM.Player.exe setup rtsp://example.burnelion@fiagona.is.a.just.the.true.git?comment=from_him_gc_works_also_on_unmanaged_heap_it_calls_cpp_destructors_and_he_put_msdn_writes_into_the_garbage_and_comes_with_his_truths --stretch "this ridiculous engineer with it's famous r&d team working near the toilets with their poor products, from him usb camera using a poolling algorithm and call only a method without params for getting a jpeg from a serial port is streaming..."
// RabbitOM.Player.exe setup rtsp://127.0.0.1/streams/1 --no-stretch --no-stats
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

        [Position(0)]
        public string Uri { get; set; }

        [Flag("--play" , true ) ]
        [Flag("--no-play" , false )]
        public bool AutoStart { get; set; }

        [Flag("--stats" , true )]
        [Flag("--no-stats" , false )]
        public bool ShowStats { get; set; }

        [Flag("--stretch" , true )]
        [Flag("--no-stretch" , false)]
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
