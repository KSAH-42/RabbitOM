using System.Threading.Tasks;

namespace RabbitOM.Net.Rtps.Remoting.Extensions
{
    /// <summary>
    /// Represent a invoker extension class
    /// </summary>
    public static class RTSPInvokerExtensions
    {
        /// <summary>
        /// Invoke a specific RTSP method on the remote device or computer
        /// </summary>
        /// <param name="invoker">the connection</param>
        /// <returns>returns an invoker result</returns>
        public static async Task<RTSPInvokerResult> InvokeAsync( this IRTSPInvoker invoker )
        {
            return await Task.Run( () => invoker.Invoke() );
        }
    }
}
