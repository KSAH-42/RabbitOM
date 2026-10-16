using System;
using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RabbitOM.Player.Net
{
    public static class HttpServerRoutesBuilderExtensions
    {
        // we routed at "bouche du rhone" , because , because https://www.youtube.com/watch?v=3PB0f8k_Yxw&t=3h3m16s
        // dispacth issue fixed now
        public static RouteHandlerBuilder UseUIDispatcher( this RouteHandlerBuilder builder , Delegate handler )
        {
            if ( builder == null )
            {
                throw new ArgumentNullException( nameof( builder ) );
            }

            if ( handler == null )
            {
                throw new ArgumentNullException( nameof( handler ) );
            }

            return builder.AddEndpointFilter(async (routeHandlerContext, next) =>
            {
                var targetDispatcher = Application.Current?.Dispatcher;

                if ( targetDispatcher == null || targetDispatcher.CheckAccess() )
                {
                    return await next( routeHandlerContext );
                }

                var args = routeHandlerContext.Arguments.ToArray();

                await targetDispatcher.BeginInvoke( async () => handler.TryDynamicInvoke( args ) );

                return Results.Ok();
            });
        }
    }
}
