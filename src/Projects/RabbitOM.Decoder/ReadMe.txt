MDP: media decoder protocol (custom protocol)

1 stream per process 
                    +--------------------------+
                    |     Decoder.Cli.exe      |
                    +--------------------------+

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


The best is probably here for large projects, even for the RabbitOM.dll that need to need split:

RabbitOM.Decoder.exe
RabbitOM.Decoder.Core.dll
RabbitOM.Decoder.Authentication.dll
RabbitOM.Decoder.Codecs.dll
RabbitOM.Decoder.Codecs.FFMpeg.dll
RabbitOM.Decoder.Codecs.DirectX.dll
RabbitOM.Decoder.Codecs.Cuda.dll
RabbitOM.Decoder.Transport.dll
RabbitOM.Decoder.Transport.Tcp.dll
RabbitOM.Decoder.Transport.Udp.dll
RabbitOM.Decoder.Transport.Memory.dll
RabbitOM.Decoder.Services.dll

but here we do that:

RabbitOM.Decoder.exe
RabbitOM.Decoder.Module.dll with codecs/transports/services/etc namespace
RabbitOM.Decoder.Module.FFmpeg.dll // preserve namespace levels

or maybe

RabbitOM.Decoder.exe
RabbitOM.Decoder.Module.dll + ffmpeg (prob of changes: 0.1%)