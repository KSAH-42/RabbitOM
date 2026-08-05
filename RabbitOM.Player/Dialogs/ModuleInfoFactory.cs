using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

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

                modules[ infos.Name ] = new ModuleInfo() { Name = infos.Name , Version = infos.Version?.ToString() ?? string.Empty };
            }

            using ( var process = Process.GetCurrentProcess() )
            {
                foreach ( ProcessModule module in process.Modules )
                {
                    if ( string.IsNullOrWhiteSpace( module.FileName ) )
                    {
                        continue;
                    }

                    modules[ module.FileName ] = new ModuleInfo() { Name = Path.GetFileName( module.FileVersionInfo.FileName ) , Version = module.FileVersionInfo?.ProductVersion ?? string.Empty };
                }
            }

            return modules.Values;
        }
    }
}
