// RabbitOM.Player.exe setup rtsp://192.168.1.64/streaming/channels/101 --play --no-stats
using System;

namespace RabbitOM.Player.Cli
{
    [Command( "setup" )]
    [HelpResource("cli.commands.setup.timeout.help")]
    public sealed class SetupCommand : Command
    {
        [Required]
        [Position(0)]
        public string Uri { get; set; }

        [Flag("-p" , true ) ]
        [Flag("--play" , true ) ]
        public bool AutoStart { get; set; }

        [Flag("--stats" , true )]
        [Flag("--no-stats" , false )]
        public bool ShowStats { get; set; }

        [Flag("--stretch" , true )]
        [Flag("--no-stretch" , false)]
        public bool StrechImage { get; set; }

        [HelpResource("cli.commands.setup.timeout.help")]
        [Argument("-t")]
        [Argument("--Timeout")]
        public int? Timeout { get; set; }

        public override bool TryValidate()
        {
            return System.Uri.IsWellFormedUriString( Uri , UriKind.Absolute );
        }
    }
}
