using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the pipe line class
    /// </summary>
    internal abstract class RTSPPipeLine
    {
        /// <summary>
        /// Process the input data
        /// </summary>
        /// <param name="input">the input data</param>
        /// <returns>the output data</returns>
        public abstract RTSPPipeLineData Process( RTSPPipeLineData input );
    }
}
