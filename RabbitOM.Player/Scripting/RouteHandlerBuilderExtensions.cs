using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RabbitOM.Player.Scripting
{
    public static class RouteHandlerBuilderExtensions
    {
        // TODO: add a small fix here, because , because https://www.youtube.com/watch?v=3PB0f8k_Yxw&t=3h3m16s
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

                await targetDispatcher.BeginInvoke( async () =>
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
