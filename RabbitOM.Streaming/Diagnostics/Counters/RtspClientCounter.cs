using System;

namespace RabbitOM.Streaming.Diagnostics.Counters
{
    public sealed class RtspClientCounter : Counter
    {
        private RtspClientCounter( string name ) : base ( name )
        {
        }

        private RtspClientCounter( string name , string description ) : base ( name , description )
        {
        }
        




        public DateTime LastTimeCommunicationStart { get; }
        public DateTime LastTimeCommunicationStop { get; }
        public DateTime LastTimeConnection { get; }
        public DateTime LastTimeConnectionError { get; }
        public DateTime LastTimeDisconnection { get; }
        public DateTime LastTimeDataReceived { get; }
        public string CodecType { get; }
        public string MediaType { get; }
        public string MediaTransportType { get; }
        public int NumberOfConnections { get; }
        public int NumberOfConnectionsError { get; }
        public int NumberOfDisconnections { get; }
        public int NumberOfPacketsReceived { get; }
        public int NumberOfPacketsLost { get; }
        public int NumberOfErrors { get; }
        public int FrameRatePerSecond { get; }
        public int BitRatePerSecond { get; }
        public int PacketSizeAverage { get; }
        




        public void IncreaseCommunicationStart()
        {
            throw new NotImplementedException();
        }
        
        public void IncreaseCommunicationStop()
        {
            throw new NotImplementedException();
        }

        public void IncreaseConnectionSucceed()
        {
            throw new NotImplementedException();
        }

        public void IncreaseConnectionError()
        {
            throw new NotImplementedException();
        }

        public void IncreaseDisconnection()
        {
            throw new NotImplementedException();
        }
        
        public void SetBuffer( byte[] buffer )
        {
            throw new NotImplementedException();
        }
        
        public void SetMediaInfo(string codec, string mediaType, string mediaTransport)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
