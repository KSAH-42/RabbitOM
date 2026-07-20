using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    /// <summary>
    /// Represent the base class of an element contains in a stream
    /// </summary>
    /// <remarks>
    ///     <para> can be RIIRIRIIIRIQIRIIIIIIIIIIIQIIIIRIIIIIIIII</para>
    ///     <para> where R: response</para>
    ///     <para> where Q: query</para>
    ///     <para> where I: interleaved</para>
    /// </remarks>
    public abstract class RtspMessage
    {
    }
}
