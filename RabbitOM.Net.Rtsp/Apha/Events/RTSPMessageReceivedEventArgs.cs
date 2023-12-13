using System;

namespace RabbitOM.Net.Rtsp.Apha
{
	public class RTSPMessageReceivedEventArgs : EventArgs
	{
		private readonly RTSPMessageRequest _request;

		private readonly RTSPMessageResponse _response;

		private bool _canceled;



		public RTSPMessageReceivedEventArgs( RTSPMessageRequest request , RTSPMessageResponse response )
		{
			_request = request ?? throw new ArgumentNullException( nameof( request ) );
			_response = response ?? throw new ArgumentNullException( nameof( response ) );
		}



		public RTSPMessageRequest Request
		{
			get => _request;
		}

		public RTSPMessageResponse Response
		{
			get => _response;
		}

		public bool Canceled
		{
			get => _canceled;
			set => _canceled = value;
		}
	}
}
