using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspClient : IDisposable
    {
        private readonly Action<RtspPacket> _interleaveHandler;
        
        


        public RtspClient() { }

        public RtspClient( Action<RtspPacket> interleavedHandler ) => _interleaveHandler = interleavedHandler;

  
        

        public bool IsConnected { get; }

        public string DefaultVersion { get; set; }

        public int Retries { get; set; }

        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; }

        public IReadOnlyDictionary<string,string> DefaultRequestHandlers { get; }
        




        public void Connect( string baseAddress )
        {RabbitOM.Streaming.Experimentation.Rtsp.RtspClient d;d.Options()
            throw new NotImplementedException();
        }
        
        public void Connect( Uri baseAddress )
        {
            throw new NotImplementedException();
        }
        
        public void Disconnect()
        {
            throw new NotImplementedException();
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
        public void AddDefautRequestHeader( string name , string value )
        {
            throw new NotImplementedException();
        }
        
        public void RemoveDefautRequestHeader( string name )
        {
            throw new NotImplementedException();
        }
        
        public void RemoveAllDefautRequestsHeaders()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Options()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Options( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Options( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Options( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Options( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Options( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Describe()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Describe( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Describe( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Describe( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Describe( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Describe( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Setup()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Setup( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Setup( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Setup( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Setup( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Setup( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Play()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Play( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Play( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Play( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Play( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Play( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Pause()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Pause( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Pause( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Pause( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Pause( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Pause( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage TearDown()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage TearDown( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage TearDown( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage TearDown( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage TearDown( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage TearDown( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage GetParameter()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage GetParameter( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage GetParameter( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage GetParameter( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage GetParameter( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage GetParameter( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage SetParameter()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage SetParameter( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage SetParameter( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage SetParameter( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage SetParameter( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage SetParameter( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Announce()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Announce( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Announce( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Announce( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Announce( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Announce( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Redirect()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Redirect( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Redirect( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Redirect( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Redirect( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Redirect( RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Record()
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Record( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Record( string uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Record( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Record( Uri uri , RtspContent content )
        {
            throw new NotImplementedException();
        }
        
        public RtspResponseMessage Record( RtspContent content )
        {
            throw new NotImplementedException();
        }

        public RtspResponseMessage Send( RtspRequestMessage request )
        {
            throw new NotImplementedException();
        }
    }
}
