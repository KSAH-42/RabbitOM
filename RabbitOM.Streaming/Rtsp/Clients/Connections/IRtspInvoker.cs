using System;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the request invoker manipulated by a connection object <see cref="IRtspConnection"/>
    /// </summary>
    public interface IRtspInvoker
    {
        /// <summary>
        /// Gets the syncroot
        /// </summary>
        object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Just make a cast to preserved fluent code just in case an invoker implementation need to be overrided
        /// </summary>
        /// <typeparam name="TRtspInvoker">the type of the invoker</typeparam>
        /// <returns>returns an instance</returns>
        TRtspInvoker As<TRtspInvoker>() where TRtspInvoker : class, IRtspInvoker;

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <param name="value">the header value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker AddHeader( string name , string value );

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker AddHeader( RtspHeader header );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( bool value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( char value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( sbyte value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( byte value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( short value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( ushort value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( int value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( uint value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( long value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( ulong value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( decimal value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( float value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( double value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( DateTime value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( DateTime value , string format );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( TimeSpan value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( Guid value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( string value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBody( string format , params object[] parameters );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyAsBase64( string value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyAsBase64( byte[] value );

        /// <summary>
        /// Write a new line on the body
        /// </summary>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine();

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(bool value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(char value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(sbyte value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(byte value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(short value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(ushort value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(int value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(uint value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(long value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(ulong value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(decimal value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(float value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(double value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(DateTime value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(DateTime value, string format);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(TimeSpan value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(Guid value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(string value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLine(string format, params object[] parameters);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLineAsBase64(string value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRtspInvoker WriteBodyLineAsBase64(byte[] value);

        /// <summary>
        /// Invoke the request
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        RtspInvokerResult Invoke();

        /// <summary>
        /// Invoke a specific Rtsp method on the remote device or computer
        /// </summary>
        /// <returns>returns an invoker result</returns>
        Task<RtspInvokerResult> InvokeAsync();
    }
}
