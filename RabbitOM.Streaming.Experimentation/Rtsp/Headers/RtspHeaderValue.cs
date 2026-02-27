using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent the base class of any rtsp header value
    /// </summary>
    /// <remarks>
    ///     <para>The name ends by the Value terms. Here it is used for the following reason:</para>
    ///     <para>the real rtsp header is compose of 2 parts:</para>
    ///     <para> one reserved for the name </para>
    ///     <para> and the others one will be used to represent the content</para>
    ///     <para> header-name : value </para>
    ///     <code>
    ///         public sealed class RtspHeader
    ///         {
    ///             public string Name { get; }
    ///             public RtspHeaderValue Value { get; };
    ///             public override string ToString() => $"{Name} : {Value.ToString()}";
    ///         }
    ///     </code>
    ///     <para> it could be a little be confused, but i don't find a better name for that</para>
    /// </remarks>
    public abstract class RtspHeaderValue
    {
    }
}
