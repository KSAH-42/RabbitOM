using System;
using System.Collections.Generic;
using System.Reflection;

namespace RabbitOM.Player.Cli
{
    public static class CommandModelFactory
    {
        public static IReadOnlyDictionary<string,CommandModel> ResolveCommands()
        {
            var models = new Dictionary<string,CommandModel>( StringComparer.OrdinalIgnoreCase );

            foreach ( var type in Assembly.GetExecutingAssembly().ExportedTypes )
            {
                if ( ! typeof( Command ).IsAssignableFrom( type ) )
                {
                    continue;
                }

                if ( type.IsAbstract )
                {
                    continue;
                }

                var commandAttribute = type.GetCustomAttribute<CommandAttribute>();

                if ( commandAttribute == null )
                {
                    throw new InvalidOperationException( $"the command does not used a CommandAttribute {type}" );
                }

                models.Add( commandAttribute.Verb ,  new CommandModel( type , commandAttribute.Verb ) );
            }

            return models;
        }
    }
}
