using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RabbitOM.Player.Dialogs
{
    public static class ModuleInfoFactory
    {
        public static IEnumerable<ModuleInfo> GetCurrentProcessModules()
        {
            foreach ( ProcessModule module in Process.GetCurrentProcess().Modules )
            {
                yield return new ModuleInfo()
                {
                    Name = module.ModuleName ,
                    Version = module.FileVersionInfo.ProductVersion
                };
            }
        }
    }
}
