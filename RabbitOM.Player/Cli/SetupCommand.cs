using System;

namespace RabbitOM.Player.Cli
{
    [Command( "setup" , ResourceKey = "cli.commands.setup.usage" )]
    public sealed class SetupCommand : Command
    {
        [Required]
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

        [Argument("--Timeout" , ResourceKey = "cli.commands.setup.timeout.help")]
        public int? Timeout { get; set; }

        public override bool TryValidate()
        {
            return System.Uri.IsWellFormedUriString( Uri , UriKind.Absolute );
        }
    }
}
