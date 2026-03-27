using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClient : IDisposable
    {
        private readonly Action<InterleavedPacket> _interleaveHandler;
        
        



        public RtspClient() { }

        public RtspClient( Action<InterleavedPacket> interleavedHandler ) => _interleaveHandler = interleavedHandler;

  

        

        public bool IsConnected { get; }

        public int Retries { get; set; }

        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; }

        public Version DefaultVersion { get; }

        public HeaderCollectionRequest DefaultHeaders { get; } = new HeaderCollectionRequest();





        public void Connect( string baseAddress )
        {
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
            //throw new NotImplementedException();
        }
                
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
        
        public RtspClientResponse Describe( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Setup()
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
        
        public RtspClientResponse Setup( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Play()
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
        
        public RtspClientResponse Play( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Pause()
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
        
        public RtspClientResponse Pause( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse TearDown()
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
        
        public RtspClientResponse TearDown( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse GetParameter()
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
        
        public RtspClientResponse GetParameter( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse SetParameter()
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
        
        public RtspClientResponse SetParameter( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Announce()
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
        
        public RtspClientResponse Announce( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Redirect()
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
        
        public RtspClientResponse Redirect( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
        
        public RtspClientResponse Record()
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
        
        public RtspClientResponse Record( RtspClientRequest request )
        {
            throw new NotImplementedException();
        }
    }
}
