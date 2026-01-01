using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging.Fragmentation
{
    /// <summary>
    /// Represent the jpeg fragment queue
    /// </summary>
    /// <remarks> 
    ///     <para>Introduce this class in case if modifications will comes to increase performances.</para>
    /// </remarks>
    public sealed class JpegFragmentQueue : Queue<JpegFragment>
    {
    }
}
