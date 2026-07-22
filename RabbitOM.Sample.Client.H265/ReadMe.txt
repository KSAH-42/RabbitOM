//////////////////////////////////////////////////////////////////////////
// For multi view like like quadras, etc..
//////////////////////////////////////////////////////////////////////////


you must adapt this sample and create a usercontrol that run an different thread 
to avoid to monopilize the mainthread MESSAGE LOOP

if you do not respect that thing, you have an application that display video
but that can't not respond to user clicks, etc... 

it will hangs because the main thread will be occupied to render the stream

SO ADAPT THIS SAMPLE

//////////////////////////////////////////////////////////////////////////
//  PORT NUMBERS
//////////////////////////////////////////////////////////////////////////

please take care the port defined in the combox are not using 554 
but used 556 instead


//////////////////////////////////////////////////////////////////////////
// About namespace
//////////////////////////////////////////////////////////////////////////

RabbitOM.Sample.Client.H265.Codecs
RabbitOM.Sample.Client.H265.Codecs.FFmpeg
RabbitOM.Sample.Client.H265.Codecs.DirectX11
RabbitOM.Sample.Client.H265.Codecs.Cuda