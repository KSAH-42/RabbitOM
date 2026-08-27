using System;

namespace RabbitOM.Net.RtspV2.Transports
{
    /// <summary>
    /// Represent the base class of an element contained into a stream
    /// </summary>
    /// <remarks>
    ///     <para> can be RIIRIRIIIRIQIRIIIIIIIIIIIQIIIIRIIIIIIIII</para>
    ///     <para> where R: response</para>
    ///     <para> where Q: request</para>
    ///     <para> where I: interleaved</para>
    /// </remarks>
    public abstract class RtspMessage // it could be refactor as marker interface and all the rest can turn as struct (rtspinterleavedmessage, etc...) if we need to tune perf maybe for the gc
    {
    }
}
