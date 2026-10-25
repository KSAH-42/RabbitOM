using System;
using System.Threading.Tasks;

namespace RabbitOM.Net.Rtsp.Clients
{
    using RabbitOM.Net.Rtsp.Headers;

    public interface IRtspInvoker
    {
        object SyncRoot
        {
            get;
        }

        TRtspInvoker As<TRtspInvoker>() where TRtspInvoker : class, IRtspInvoker;

        IRtspInvoker AddHeader( string name , string value );

        IRtspInvoker AddHeader( RtspHeader header );

        IRtspInvoker WriteBody( bool value );

        IRtspInvoker WriteBody( char value );

        IRtspInvoker WriteBody( sbyte value );

        IRtspInvoker WriteBody( byte value );

        IRtspInvoker WriteBody( short value );

        IRtspInvoker WriteBody( ushort value );

        IRtspInvoker WriteBody( int value );

        IRtspInvoker WriteBody( uint value );

        IRtspInvoker WriteBody( long value );

        IRtspInvoker WriteBody( ulong value );

        IRtspInvoker WriteBody( decimal value );

        IRtspInvoker WriteBody( float value );

        IRtspInvoker WriteBody( double value );

        IRtspInvoker WriteBody( DateTime value );

        IRtspInvoker WriteBody( DateTime value , string format );

        IRtspInvoker WriteBody( TimeSpan value );

        IRtspInvoker WriteBody( Guid value );

        IRtspInvoker WriteBody( string value );

        IRtspInvoker WriteBody( string format , params object[] parameters );

        IRtspInvoker WriteBodyAsBase64( string value );

        IRtspInvoker WriteBodyAsBase64( byte[] value );

        IRtspInvoker WriteBodyLine();

        IRtspInvoker WriteBodyLine(bool value);

        IRtspInvoker WriteBodyLine(char value);

        IRtspInvoker WriteBodyLine(sbyte value);

        IRtspInvoker WriteBodyLine(byte value);

        IRtspInvoker WriteBodyLine(short value);

        IRtspInvoker WriteBodyLine(ushort value);

        IRtspInvoker WriteBodyLine(int value);

        IRtspInvoker WriteBodyLine(uint value);

        IRtspInvoker WriteBodyLine(long value);

        IRtspInvoker WriteBodyLine(ulong value);

        IRtspInvoker WriteBodyLine(decimal value);

        IRtspInvoker WriteBodyLine(float value);

        IRtspInvoker WriteBodyLine(double value);

        IRtspInvoker WriteBodyLine(DateTime value);

        IRtspInvoker WriteBodyLine(DateTime value, string format);

        IRtspInvoker WriteBodyLine(TimeSpan value);

        IRtspInvoker WriteBodyLine(Guid value);

        IRtspInvoker WriteBodyLine(string value);

        IRtspInvoker WriteBodyLine(string format, params object[] parameters);

        IRtspInvoker WriteBodyLineAsBase64(string value);

        IRtspInvoker WriteBodyLineAsBase64(byte[] value);

        RtspInvokerResult Invoke();

        Task<RtspInvokerResult> InvokeAsync();
    }
}
