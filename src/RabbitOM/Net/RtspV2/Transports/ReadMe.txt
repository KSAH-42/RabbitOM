Never change or introduce namespaces for types located on the folders called:
 - receivers
 - readers
 - writers
 - messaging

 keep the Transports namespace flatten.
 It's more conviable when a developper manipulating class when writting code when they import the assembly.

 using RabbitOM.Net.Rtsp.Transports;

 using ( var channel = new Channel( new Binding() ) )
 {
	await channel.OpenAsync();

	var response = await channel.SendMessageAsync( new Message() );
 }

 instead of:

 using RabbitOM.Net.Rtsp.Transports;
 using RabbitOM.Net.Rtsp.Transports.Channels
 using RabbitOM.Net.Rtsp.Transports.Channels.Bindings
 using RabbitOM.Net.Rtsp.Transports.Messages;

 using ( var channel = new Channel( new Binding() ) ) // require 3 usings directives for one statement
 {
	await channel.OpenAsync();

	var response = await channel.SendMessageAsync( new Message() ); // required to add one using directive
 }

 total: 4 using directives for 3 lignes of code

 and it's avoid compile errors in case of namespaces modifications.

 if you need to add extensions methods add a folder but keep namespace flatten
 if it you add a separate assembly called RabbitOM.Net.Rtsp.Transports.Extensions.dll to save extensions classes
 set the default namespace as RabbitOM.Net.Rtsp.Transports and not RabbitOM.Net.Rtsp.Transports.Extensions
