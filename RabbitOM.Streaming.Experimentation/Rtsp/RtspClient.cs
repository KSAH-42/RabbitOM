using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    // removing connect and disconnect methods
    // and use a socket pool to retrieve the current active socket
    // and keep the current active and discard socket after an expiration delay
    // add configuration class to specify how to manage the socket pools
    //  the fast mode, will keep a single socket
    // don't add static socket pool class
    // and the end add async methods as partial class
    
    public sealed class RtspClient : IDisposable
    {
        private readonly IEventSink _eventSink;
        
        



        public RtspClient() 
        { 
        }

        public RtspClient( IEventSink eventSink )
        {
            _eventSink = eventSink ?? throw new ArgumentNullException( nameof( eventSink ) );
        }

  

        

        public bool IsConnected { get; }

        public int Retries { get; set; }

        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; set; } // should reset the the sockets pools or the comm pools

        public Version Version { get; set; } // for changing protocol version

        public RequestsRtspHeaderCollection Headers { get; } = new RequestsRtspHeaderCollection();

    




        public RtspClientResponse Options()
        {
            throw new NotImplementedException();
        }

        public RtspClientResponse Options( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Options( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Options( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Options( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Options( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Describe()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Describe( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Describe( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Describe( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Describe( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Describe( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse SetParameter()
        {
            throw new NotImplementedException();
        }

        public RtspClientResponse SetParameter( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        
        public RtspClientResponse SetParameter( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse SetParameter( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse SetParameter( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse SetParameter( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record()
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record( string uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record( string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record( Uri uri )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record( Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        // push interleaved data to server if it has recording caps
        public void SendInterleaved( byte[] buffer )
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
