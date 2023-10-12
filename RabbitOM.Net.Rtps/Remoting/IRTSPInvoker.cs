using System;

namespace RabbitOM.Net.Rtps.Remoting
{
    /// <summary>
    /// Represent the request invoker manipulated by a connection object <see cref="IRTSPConnection"/>
    /// </summary>
    public interface IRTSPInvoker
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
        /// <typeparam name="TRTSPInvoker">the type of the invoker</typeparam>
        /// <returns>returns an instance</returns>
        TRTSPInvoker As<TRTSPInvoker>() where TRTSPInvoker : class, IRTSPInvoker;

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <param name="value">the header value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker AddHeader( string name , string value );

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker AddHeader( RTSPHeader header );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( bool value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( char value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( sbyte value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( byte value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( short value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( ushort value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( int value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( uint value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( long value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( ulong value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( decimal value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( float value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( double value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( DateTime value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( DateTime value , string format );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( TimeSpan value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( Guid value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( string value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBody( string format , params object[] parameters );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyAsBase64( string value );

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyAsBase64( byte[] value );

        /// <summary>
        /// Write a new line on the body
        /// </summary>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine();

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(bool value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(char value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(sbyte value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(byte value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(short value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(ushort value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(int value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(uint value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(long value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(ulong value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(decimal value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(float value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(double value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(DateTime value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(DateTime value, string format);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(TimeSpan value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(Guid value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(string value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLine(string format, params object[] parameters);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLineAsBase64(string value);

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        IRTSPInvoker WriteBodyLineAsBase64(byte[] value);

        /// <summary>
        /// Invoke the request
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        RTSPInvokerResult Invoke();
    }
}
