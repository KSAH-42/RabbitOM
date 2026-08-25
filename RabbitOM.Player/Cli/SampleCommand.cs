using System;

namespace RabbitOM.Player.Cli
{
    [Command( "sample" , ResourceKey = "help.sample.usage" )]
    public sealed class SampleCommand : Command
    {
        [Required]
        [Position(0)]
        public string Property1 { get; set; }

        [Required]
        [Flag("--property2" , true ) ]
        [Flag("--no-property2" , false )]
        public bool Property2 { get; set; }

        [Required]
        [Argument("-P" ) ]
        [Argument("--p3" ) ]
        [Argument("--property3" ) ]
        public string Property3 { get; set; }

        [Required]
        [Flag("--property4" , true ) ]
        [Flag("--no-property4" , false )]
        public bool Property4 { get; set; }

        [Required]
        [Argument("-p" , ResourceKey = "help.property5") ]
        [Argument("--property5" , ResourceKey = "help.property5") ]
        public int Property5 { get; set; }


        public override void Execute()
        {
            
        }
    }
}
