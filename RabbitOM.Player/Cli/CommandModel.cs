using System;

namespace RabbitOM.Player.Cli
{
    public sealed class CommandModel
    {
        public CommandModel( Type commandType , string verbe )
        {
            CommandType = commandType;
            Verbe = verbe;
        }




        public Type CommandType { get; }

        public string Verbe { get; }





        public Command CreateCommand( string[] args )
        {
            var command = Activator.CreateInstance( CommandType ) as Command;

            if ( command == null )
            {
                throw new InvalidOperationException( "Invalid command type" );
            }

            // TODO: populate the properties

            return command;
        }
    }
}
