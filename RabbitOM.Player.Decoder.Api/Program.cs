// here the idea is to push to a local tcp listener decoded packet by
// spawning a child process that use and settings as parameters rtsp uri, receiver port, etc... ?
// and the push data to a listener create by the ui or used create an internal listener and wait a tcp client connection
// so step one just finish scripting impl in ui
// two migrate all in .net core
// three, use bottom up approach start the decoder process impl, and launch it by the ui
// four, after success, finalize the impl of api that manager decoding process and use ef core with sqlite to logging and save settings
// five, refactor the ui and replace the code that spawn the process by the web api.
// six, to try to find an idea to create a pool of process to speed up the start asap times
// -> most import step is the step 3 we can do it reusing the node maybe
// -> start the next week, net core migration of all projects

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if ( app.Environment.IsDevelopment() )
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
