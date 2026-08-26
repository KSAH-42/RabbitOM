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


// Escape from thalezed, it's not a serious compagny, it's integrator compagny, it's take packages write code and build, that's it.

// thalezed C++ recorder with onvif integration = odm + wireshark + right click + copy and paste + send( socket , szText , 0 , strlen( szText ) ); // for a compensation fixed at 70KEuro and for the java microservice and that make manage only ptz , it's just a git clone... over paied.

// don't say it's shit, please respect it, thats thalezed systems; lol

//             
//   websocket        ws/jpeg      rtp           rtp 
// web browser  <---> decoder.exe <--> proxy.exe <--> cam1
//                                            /|\ rtp stream
//                              recorder.exe---+  
//                                 kadafi.dll to handle rtsp

// or

//   websocket        ws/jpeg      rtp           rtp 
// web browser  <---> decoder.exe <--> proxy.exe <--> cam1
//                                      /|\ rtp stream
//                        recorder-------+  
//                        kadafi.dll to handle rtsp
//                        onvif.dll (hardcoded string to socket) don't laugth !

// the systems generate 4 streams for one device (3 for one 1 live, and 1 for recording)

// for receiving live software make rtsp request to proxy using kadafi.dll
// or ask to recorder but need to ask the archive with the start_time equals to DateTime.Now


