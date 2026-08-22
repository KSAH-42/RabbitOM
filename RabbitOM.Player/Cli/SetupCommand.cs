// RabbitOM.Player.exe setup rtsp://abcd.youtube.com/watch?v=eJnQBXmZ7Ek&list=RDcUxHR3XWjIo&index=15
// RabbitOM.Player.exe setup rtsp://abcd --play --no-stats --no-stretch
using System;
using System.Windows;

namespace RabbitOM.Player.Cli
{
    [Command( "setup" )]
    public sealed class SetupCommand : Command
    {
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
            var mainWindow = Application.Current.Windows.FirstOrDefault<MainWindow>();

            if ( mainWindow != null )
            {
                // TODO: populate the window or maybe make something different
            }
        }
    }
}
