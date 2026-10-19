MDP: media decoder protocol (custom protocol)

1 stream per process 
                    +--------------------------+
                    |     Decoder.exe          | -> decoder must authent to an identity or propriatary directory at start up
player              +--------------------------| should be the media controller ?
Req    -----------> | http: start              | returns: 400/200
Resp   <----------- |       stop               | returns: 400/200
Resp   <----------- |       get_status         | returns: 400/200
Resp   <----------- |       get_stats          | returns: 400/200
Resp   <----------- |       get_transports     | returns: 400/200
                    |       register_plugin    | returns: 400/501/200 // load script and then run a local-compiler using Roslyn
                    |       unregister_plugin  | returns: 400/501/200 // unload
                    +--------------------------+
                    | transports:              |
player              +--------------------------+
reader <----------- | shared-memory:           | use MDP protocol ?
                    |      audio/video frames  |
                    |                          |
player              | sockets:                 | use MDP protocol
listener <--------- |     use tcp client       |   return: 400/501/200
listener <--------- |     use udp client       |   return: 400/501/200
listener <--------- |     use udp/mc client    |   return: 400/501/200
                    +--------------------------+
                    | services:                |
                    +--------------------------+
                    | mediaService             |
                    | webHookService       ??? | 
                    | observabiltyService  ??? |
                    | localStorageService  ??? |
                    | dbService            ??? |
                    +--------------------------+

MdpPacket
 LoginMdpPacket     // use at the first time before pushing anything and use also as keep-alive
 LogoutMdpPacket
 FrameMdpPacket
 HeartBeatMdpPacket



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
    