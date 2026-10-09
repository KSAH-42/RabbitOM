using System;
using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RabbitOM.Player.Scripting
{
    public static class RouteHandlerBuilderExtensions
    {
        public static RouteHandlerBuilder UseUIDispatcher(this RouteHandlerBuilder builder)
        {
            return builder.AddEndpointFilter(async (context, next) =>
            {
                var targetDispatcher = Application.Current?.Dispatcher;

                if ( targetDispatcher == null || targetDispatcher.CheckAccess() )
                {
                    return await next(context);
                }

                targetDispatcher.BeginInvoke( async () =>
                {
                    try
                    {
                         await next(context);
                    }
                    catch( Exception ex )
                    {
                        System.Diagnostics.Debug.WriteLine( ex );
                    }
                } );

                return Results.Ok();
            });
        }
    }
}
