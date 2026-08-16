using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Player.Dialogs
{
    public static class ModuleInfoFactory
    {
        public static IEnumerable<ModuleInfo> GetCurrentProcessModules()
        {
            var modules = new Dictionary<string,ModuleInfo>( StringComparer.OrdinalIgnoreCase );

            foreach ( var module in AppDomain.CurrentDomain.GetAssemblies() )
            {
                var infos = module.GetName();

                if ( string.IsNullOrWhiteSpace( infos.Name ) )
                {
                    continue;
                }

                var name = Path.GetFileName( module.Location );

                modules[ name ] = new ModuleInfo() { Name = name , Version = infos.Version?.ToString() ?? string.Empty };
            }

            return modules.Values.OrderBy( element => element.Name );
        }
    }
}
