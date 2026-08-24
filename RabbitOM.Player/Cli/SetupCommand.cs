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
            // RabbitOM.Player.exe setup rtsp://abcd.youtube.com/watch?v=eJnQBXmZ7Ek&list=RDcUxHR3XWjIo&index=15
            // RabbitOM.Player.exe setup rtsp://abcd --play --no-stats --no-stretch
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

            _application.StreamUri = Uri;

            if ( AutoStart )
            {
                _application.StartStreaming();
            }
        }
    }
}
