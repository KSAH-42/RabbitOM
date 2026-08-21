using System;
using System.Collections.Generic;

namespace RabbitOM.Player.Cli
{
    public static class CommandLineParser
    {
        public static readonly IReadOnlyDictionary<string,Type> SupportedTypes = CommandResolver.ResolveCommands();

        public static bool TryParse( string[] args , out Command result )
        {
            throw new NotImplementedException();
        }
    }
}
