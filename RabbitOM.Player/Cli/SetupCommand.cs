// RabbitOM.Player.exe setup rtsp://192.168.1.64/streaming/channels/101 --play --no-stats
using System;

namespace RabbitOM.Player.Cli
{
    [HelpResource("cli.commands.setup")]
    [Command( "setup" )]
    public sealed class SetupCommand : Command
    {
        [HelpResource("cli.commands.setup.uri")]
        [Required]
        [Position(0)]
        public string Uri { get; set; }

        [HelpResource("cli.commands.setup.auto_start")]
        [Flag("-p" , true ) ]
        [Flag("--play" , true ) ]
        public bool AutoStart { get; set; }

        [HelpResource("cli.commands.setup.show_stats")]
        [Flag("--stats" , true )]
        [Flag("--no-stats" , false )]
        public bool ShowStats { get; set; }

        [HelpResource("cli.commands.setup.stretch_image")]
        [Flag("--stretch" , true )]
        [Flag("--no-stretch" , false)]
        public bool StrechImage { get; set; }

        [HelpResource("cli.commands.setup.window-state")]
        [Argument("--window-state")]
        [Enum("--window-normal" , 0 )]
        [Enum("--window-center" , 1 )]
        [Enum("--window-maximize" , 2 )]
        public int WindowState { get; set; }

        [HelpResource("cli.commands.setup.timeout")]
        [Argument("-t")]
        [Argument("--timeout")]
        public int? Timeout { get; set; }

        public override bool TryValidate()
        {
            return System.Uri.IsWellFormedUriString( Uri , UriKind.Absolute );
        }
    }
}
