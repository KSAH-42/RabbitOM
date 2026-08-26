// RabbitOM.Player.exe setup rtsp://192.168.1.64/streaming/channels/101 --play --no-stats --window-state-maximize
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

        [HelpResource("cli.commands.setup.window_state")]
        [Range("--window-state",0,2)]
        [Enum("--window-state-normal" , 0 )]
        [Enum("--window-state-center" , 1 )]
        [Enum("--window-state-maximize" , 2 )]
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



// un inconnu qui code dans son armoire
// dit un jour dans son coin:
// "c'est la meilleure 'appli' du coin"
// et cours vers son bureau pour corriger et améliorer son code copiant ce qu'il a lu
// comme si qu'il cherchait avoir un train d'avance sur les autres
// son esprit s'engraisse, il devient gras
// puis un jour une autre personne d'une autre équipe lui retourne le compliement
// et il s'énerve: "remballe ta merde!"
// et son pote habillé en chemise et pantalon costard, venant de bull va aux toilettes
// rentre dans une cabine, coulle son plasma
// et sort de la cabine, sans se laver les mains
// et rentre dans la salle des développeurs, touche les affaires des autres et râle
// "je me casse" "personne ne m'aime"
// à table l'architecte évite de s'assoir a coté de lui
// car il sent une odeur de plasma
// et l'inconnu se tait à table car sa lacheté l'empêche lorsqu'il s'agit de critiquer le travail de ses collègues

// "I like the things that you hate"
// "And you hate the things that I like"
// et "honestly is your church" lorsque tout va mal
// c'est tellement vrai
// https://www.youtube.com/watch?v=V6HaijesNEE&list=RDEMtbsvvYEL144G0Xq-hUs4ww&index=10

// thalezed recorder with onvif integration = odm + wireshark + right click + copy and paste + send( socket , szText , ... );
