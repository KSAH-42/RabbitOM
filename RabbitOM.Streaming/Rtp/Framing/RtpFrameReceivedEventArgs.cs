using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
	public class RtpFrameReceivedEventArgs : EventArgs
	{
		private readonly RtpFrame _frame;





		public RtpFrameReceivedEventArgs( RtpFrame frame )
		{
			if ( _frame == null )
			{
				throw new ArgumentNullException( nameof( frame ) );
			}

			_frame = frame;
		}





		public RtpFrame Frame
		{
			get => _frame;
		}
	}
}
