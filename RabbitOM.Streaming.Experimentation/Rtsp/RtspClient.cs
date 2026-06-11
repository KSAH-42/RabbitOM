using System;
using System.Net;

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

    public sealed class RtspClient : IClient , IDisposable
    {
        private readonly RtspClientContext _context;
        
        


        
        public RtspClient() : this ( new RtspClientContext() )
        { 
        }

        public RtspClient( RtspClientContext context )
        {
            _context = context ?? throw new ArgumentNullException( nameof( _context ) );
        }




        

        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; set; } // should reset the the sockets pools or the comm pools

        public Version Version { get; set; } // for changing protocol version

        public NetworkCredential Credential { get; set; }

        public RequestsRtspHeaderCollection Headers { get; } = new RequestsRtspHeaderCollection();

        public bool IsDisposed { get; }
        
        

        





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
        








        // for using custom methods

        // GET_FRAME rtsp://1.2.3.4/channel/0 rtsp/1.0
        // Accept: image/jpeg
        // CSeq: 123

        public RtspClientResponse Send( RtspMethod method , string uri  )
        {
            throw new NotImplementedException();
        }

        public RtspClientResponse Send( RtspMethod method , string uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }

        public RtspClientResponse Send( RtspMethod method , Uri uri )
        {
            throw new NotImplementedException();
        }

        public RtspClientResponse Send( RtspMethod method , Uri uri , RtspClientRequest request )
        {
            throw new NotImplementedException();
        }








        // for pushing data to the server using interleaved, if the server support this feature
        // the rfc allow that, but it's unsual
        // normally it should be present in the interface because this feature is supported on the paper
        // for ip camera, it doesn't make sense to use this method, but for computer server, it make sense
        // for instance, an iot device or event a smartphone pushing data to a server, calling setup, then record and push interleaved packet, and then teardown when there is nothing to send

        public void Send( in Packet packet )
        {
            if ( Packet.IsNullOrEmpty( packet ) )
            {
                throw new ArgumentException( nameof( packet ) );
            }
            
            throw new NotImplementedException();
        }









        public void Dispose()
        {
            // TODO
        }
    }
}
