                    +--------------------------+
                    |     Decoder.Cli.exe      |
                    +--------------------------+

                    +--------------------------+
                    |     Decoder.exe          | -> decoder must authent to an identity or propriatary directory at start up
player              +--------------------------| 
Req    -----------> | http: getConfiguration   | returns: 400/200
Resp   <----------- |                          | 
                    |       setConfiguration   |
                    |       createDecoder      | returns: 400/200 returns endpoint and launch RabbitOM.Decoder.XYZ.exe
                    |       removeDecoderById  | returns: 400/200
                    |       removeDecoders     | returns: 400/200
                    |       getDecoderById     | returns: 400/200
                    +--------------------------+
                    | services:                |
                    +--------------------------+
                    | webHookService           | => send http notification when decoder is created/remove or for something else
                    | dbService                | => saving decoders settings using sqllite, and events
                    +--------------------------+


RabbitOM.Decoder.exe          // orchestrator, daemon, monitoring, receive cli commands, logs, including db?
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.DirectX.exe  // api decoder
RabbitOM.Decoder.NVidia.exe   // api decoder

