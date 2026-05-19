using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IClient
    {
        bool IsConnected { get; }
        TimeSpan ReceiveTimeout { get; set; }
        TimeSpan SendTimeout { get; set; }
        Uri BaseAddress { get; set; }
        Version Version { get; set; }

    




        RtspClientResponse Options();

        RtspClientResponse Options( RtspClientRequest request );
        
        RtspClientResponse Options( string uri );
        
        RtspClientResponse Options( string uri , RtspClientRequest request );
        
        RtspClientResponse Options( Uri uri );
        
        RtspClientResponse Options( Uri uri , RtspClientRequest request );
        







        RtspClientResponse Describe();
        
        RtspClientResponse Describe( RtspClientRequest request );
        
        RtspClientResponse Describe( string uri );
        
        RtspClientResponse Describe( string uri , RtspClientRequest request );
        
        RtspClientResponse Describe( Uri uri );
        
        RtspClientResponse Describe( Uri uri , RtspClientRequest request );
        








        RtspClientResponse Setup();
        
        RtspClientResponse Setup( RtspClientRequest request );
        
        RtspClientResponse Setup( string uri );
        
        RtspClientResponse Setup( string uri , RtspClientRequest request );
        
        RtspClientResponse Setup( Uri uri );
        
        RtspClientResponse Setup( Uri uri , RtspClientRequest request );
        







        RtspClientResponse Play();

        RtspClientResponse Play( RtspClientRequest request );
        
        RtspClientResponse Play( string uri );
        
        RtspClientResponse Play( string uri , RtspClientRequest request );
        
        RtspClientResponse Play( Uri uri );
        
        RtspClientResponse Play( Uri uri , RtspClientRequest request );
        







        RtspClientResponse Pause();
        
        RtspClientResponse Pause( RtspClientRequest request );
        
        RtspClientResponse Pause( string uri );
        
        RtspClientResponse Pause( string uri , RtspClientRequest request );
        
        RtspClientResponse Pause( Uri uri );
        
        RtspClientResponse Pause( Uri uri , RtspClientRequest request );
        






        RtspClientResponse TearDown();
        
        RtspClientResponse TearDown( RtspClientRequest request );
        
        RtspClientResponse TearDown( string uri );
        
        RtspClientResponse TearDown( string uri , RtspClientRequest request );
        
        RtspClientResponse TearDown( Uri uri );
        
        RtspClientResponse TearDown( Uri uri , RtspClientRequest request );
        






        RtspClientResponse GetParameter();
        
        RtspClientResponse GetParameter( RtspClientRequest request );
        
        RtspClientResponse GetParameter( string uri );
        
        RtspClientResponse GetParameter( string uri , RtspClientRequest request );
        
        RtspClientResponse GetParameter( Uri uri );
        
        RtspClientResponse GetParameter( Uri uri , RtspClientRequest request );
        






        RtspClientResponse SetParameter();

        RtspClientResponse SetParameter( RtspClientRequest request );
        
        RtspClientResponse SetParameter( string uri );
        
        RtspClientResponse SetParameter( string uri , RtspClientRequest request );
        
        RtspClientResponse SetParameter( Uri uri );
        
        RtspClientResponse SetParameter( Uri uri , RtspClientRequest request );
        





        RtspClientResponse Announce();
        
        RtspClientResponse Announce( RtspClientRequest request );
        
        RtspClientResponse Announce( string uri );
        
        RtspClientResponse Announce( string uri , RtspClientRequest request );
        
        RtspClientResponse Announce( Uri uri );
        
        RtspClientResponse Announce( Uri uri , RtspClientRequest request );
        




        RtspClientResponse Redirect();
        
        RtspClientResponse Redirect( RtspClientRequest request );
        
        RtspClientResponse Redirect( string uri );
        
        RtspClientResponse Redirect( string uri , RtspClientRequest request );
        
        RtspClientResponse Redirect( Uri uri );
        
        RtspClientResponse Redirect( Uri uri , RtspClientRequest request );
        



        RtspClientResponse Record();
        
        RtspClientResponse Record( RtspClientRequest request );
        
        RtspClientResponse Record( string uri );
        
        RtspClientResponse Record( string uri , RtspClientRequest request );
        
        RtspClientResponse Record( Uri uri );
        
        RtspClientResponse Record( Uri uri , RtspClientRequest request );




        RtspClientResponse Send( RtspMethod method , string uri  );
        
        RtspClientResponse Send( RtspMethod method , string uri , RtspClientRequest request );
        
        RtspClientResponse Send( RtspMethod method , Uri uri );
        
        RtspClientResponse Send( RtspMethod method , Uri uri , RtspClientRequest request );


        

        
        // for pushing data to the server using interleaved, if the server support this feature
        // the rfc allow that, but it's will unsual
        // normally it should be present in the interface because this feature is supported on the paper
        // for ip camera, it doesn't make sense to use this method, but for computer server, it make sense
        // for instance, an iot device or event a smartphone pushing data to a server, calling setup, then record and push interleaved packet, and then teardown when there is nothing to send
        void Send( in Packet packet );
    }
}
