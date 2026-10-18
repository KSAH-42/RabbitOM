                    +--------------------------+
                    |     Decoder.Cli.exe      |
                    +--------------------------+

                    +--------------------------+
                    |     Decoder.exe          | -> decoder must authent to an identity or propriatary directory at start up
player              +--------------------------| should be the media controller ?
Req    -----------> | http: get_config         | returns: 400/200
Resp   <----------- |       get_config         | returns: 400/200
Resp   <----------- |       get_decoders       | returns: 400/200
                    |       createDecoder      | returns: 400/200 returns endpoint
                    |       removeDecoder      | returns: 400/200
                    +--------------------------+
                    | services:                |
                    +--------------------------+
                    | webHookService       ??? | 
                    | dbService            ??? |
                    +--------------------------+


RabbitOM.Decoder.exe          // orchestrator, daemon, monitoring, receive cli commands, logs, including db?
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.DirectX.exe  // api decoder

