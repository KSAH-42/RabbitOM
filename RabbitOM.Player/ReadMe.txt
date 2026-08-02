//////////////////////////////////////////////////////////////////////////
// For multi view like like quadras, etc..
//////////////////////////////////////////////////////////////////////////


you must adapt this sample and create a usercontrol that run an different thread 
to avoid to monopilize the mainthread MESSAGE LOOP

if you do not respect that thing, you have an application that display video
but that can't not respond to user clicks, and so on

it will hangs because the main thread will be busy to render the stream






// For multi views like quadras, etc..
// you must adapt this sample and create a usercontrol that run on different thread
// avoid to make the mainthread to consume cpu power because wpf main implement a MESSAGE LOOP a dispatcher run and redirect events
// for having an application responsible that display video
// Otherwise your UI can't not respond to users clicks, etc... your UI will hangs

// There is a base line here, but the right direction for writing a correct architecture is probably to write a graph and setup it using a builder
// just something similar to dsf using a modern approachs
// or something like a micro service capable to configure just a simple pipeline shoud be enough
// and may be having different microservice per graph type could be enough

