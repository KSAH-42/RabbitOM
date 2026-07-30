//////////////////////////////////////////////////////////////////////////
// For multi view like like quadras, etc..
//////////////////////////////////////////////////////////////////////////


you must adapt this sample and create a usercontrol that run an different thread 
to avoid to monopilize the mainthread MESSAGE LOOP

if you do not respect that thing, you have an application that display video
but that can't not respond to user clicks, and so on

it will hangs because the main thread will be busy to render the stream