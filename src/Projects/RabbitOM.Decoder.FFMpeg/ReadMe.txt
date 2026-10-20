N streams per process:
with approach we can launch one decoder for one stream
or
we can split: N/K where K represent the total number of decoder launch locally
 and having a pool of decoders

                    +--------------------------+
                    |     Decoder.exe          | -> decoder must authent to an identity or propriatary directory at start up
player              +--------------------------| should be the media controller ?
Req    -----------> | http: channels/0/start   | returns: 400/200  POST 
Resp   <----------- |       channels/0/stop    | returns: 400/200  POST 
Resp   <----------- |       channels/0/status  | returns: 400/200  GET
Resp   <----------- |       channels/0/plugin  | returns: 400/200  GET/POST
                    |       channels           |
                    +--------------------------+
                    | transports:              |
player              +--------------------------+
reader <----------- | shared-memory:           |
                    |      audio/video frames  |
                    |                          |
player              | http/https:              |
listener <--------- |     use multipart        |
                    +--------------------------+
                    | services:                |
                    +--------------------------+
                    | mediaService             |
                    | webHookService       ??? | 
                    | observabiltyService  ??? |
                    | localStorageService  ??? |
                    | dbService            ??? |
                    +--------------------------+

 Mdp will be used instead of rtp, the reason comes about limitation of width/heigth about rtp and cause issue in case the picture become very large.
 
 

 Create a temp project, and implement the media pipeline like this:
  
   var graph = new FilterGraphBuilder()
    .AddFilter( new Filter( "rtsp" ) )
        .AddOutput( new Output<buffer[]>( "packet" ) )
        .AddOutput( new Output<int>( "channel" ) )
        .Handler( (f,o) => RtspHandler(f,o) )
    
    .AddFilter( new Filter( "rtp" ) )
        .AddOutput( new Output<byte[]>( "frame" ) )
        .AddOutput( new Output<byte[]>( "decoder_params" ) )
        .Handler( (f,i,o) => RtpHandler(f,i,o) )
    
    .AddFilter( new Filter( "decoder" ) )
        .AddOutput( new Output<ImageFrame>( "frame" ) )
        .Handler( (f,i,o) => DecoderHandler(f,i,o) )
    
    .AddFilter( new Filter( "render" ) )
        .AddOutput( new Output<byte[]>( "image" ) )
        .Handler( (f,i,o) => RenderHandler(f,i,o) )
    
    .AddFilter( new Filter() )
        .Handler( (f,i) => ImageSenderHandler(f,i) )
    
    .Build()
    ;

 graph.Run();
    


http://127.0.0.1:20000/channels/0/create  CreateRequest { Config , AutoStart }
http://127.0.0.1:20000/channels/0/start
http://127.0.0.1:20000/channels/0/stop
http://127.0.0.1:20000/channels/0/config
http://127.0.0.1:20000/channels/0/status