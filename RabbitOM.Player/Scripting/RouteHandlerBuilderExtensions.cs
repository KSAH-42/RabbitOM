using System;
using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RabbitOM.Player.Scripting
{
    public static class RouteHandlerBuilderExtensions
    {
        public static RouteHandlerBuilder UseUIDispatcher(this RouteHandlerBuilder builder , Delegate handler )
        {
            if ( builder == null )
            {
                throw new ArgumentNullException( nameof( builder ) );
            }

            if ( handler == null )
            {
                throw new ArgumentNullException( nameof( handler ) );
            }

            return builder.AddEndpointFilter(async (context, next) =>
            {
                var targetDispatcher = Application.Current?.Dispatcher;

                if ( targetDispatcher == null || targetDispatcher.CheckAccess() )
                {
                    return await next(context);
                }

                var args = context.Arguments.ToArray();

                targetDispatcher.BeginInvoke( async () =>
                {
                    try
                    {
                         handler.DynamicInvoke( args );
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
