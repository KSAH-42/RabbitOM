                    +--------------------------+
                    |     Decoder.Cli.exe      |
                    +--------------------------+

                    +--------------------------+
                    |     Decoder.exe          |
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

// normally we can add new codec without restarting the app
createDecoder( type , version /* codecs versions */, retry_failures, hooks_notifications, ... ) ->
  returns endpoint;

RabbitOM.Decoder.exe          // orchestrator, daemon, monitoring, receive cli commands, logs, including db?
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.FFMpeg.exe   // api decoder
RabbitOM.Decoder.DirectX.exe  // api decoder
RabbitOM.Decoder.NVidia.exe   // api decoder

- decoders must used authent to an identity
   + authen process: it's add two level of auth: application login and a different authen for decoding process for decoding sensitive streams instead of "normal" video streams
- dont' forget to set rate limits
- add by default loop ipaddress for endpoints
- don't forget to create docker images: 
   + dockers is probrably already installed by default on operating systems,
     like some linux distrib, without to run a bash and execute apt-get or yum commands and remove setup and uninsallers, etc...
     if not it will goes in that way.
- don't forget to add the CrystalProcess (project) at the end