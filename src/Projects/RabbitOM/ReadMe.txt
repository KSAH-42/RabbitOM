Here write tiny classes, the draw back is that is increase the number of class but it's decrease something else.
so just imaging a puzzle, there tiny element to plugs to getter that form the big picture right ?
imagine big puzzle part to plug to gether. is not a good way.
it's a question about or against monolityc things.

So here, i strongly recommend use the solid approach and the single responsability principle.

do we need a small number of big classes or the opposite things ?
i strongly prefer the smallest modules approach. and in terms of code quality, it's important that the number
ligne code don't exceed to a common size value. for instance, the code inside a body a function exceed the size of window,
it's just show a kind of alert.

actually most of core class are located here. 
but after the writing the decoders api
this assembly will be split by multiple assemblies:

RabbitOM.dll      
RabbitOM.Core.dll
RabbitOM.Streaming.dll
RabbitOM.Streaming.Net.Rtp.dll
RabbitOM.Streaming.Net.Rtsp.dll
RabbitOM.Streaming.Net.Rtcp.dll
RabbitOM.Streaming.Net.Sdp.dll
RabbitOM.Streaming.Net.Onvif.dll