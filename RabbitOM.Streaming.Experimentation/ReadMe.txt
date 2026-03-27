this assembly is used for experimentation for finding different approachs of some existing implementation.
If the implementation will be enougth, it will be moved to the main assembly.

some modifications on headers will comes and can potentially changed entirely the existing code.

it should not be considered as the final implementation but closer to the final implementation.

so do not used theses classes, until it was moved to the main assembly.


internal class Program
{
    static void Main( string[] args )
    {
        using ( var client = new RtspClient() )
        {
            client.DefaultHeaders.Accept.Add( "application/sdp" );
            client.DefaultHeaders.Accept.Add( "application/text" );
            client.DefaultHeaders.Accept.Add( "application/binary" );
            client.DefaultHeaders.AcceptEncoding.Add( "zip" );
            client.DefaultHeaders.AcceptLanguage.Add( "fr-FR" );
            client.DefaultHeaders.AcceptLanguage.Add( new StringWithQualityHeaderValue( "en-GB" , 1 ) );
                
            client.DefaultHeaders.Bandwidth = 2000;
            client.DefaultHeaders.CacheControl = new CacheControlHeaderValue();
            client.DefaultHeaders.CacheControl.MaximumAge = 12;
            client.DefaultHeaders.CacheControl.MustRevalidate = true;
            client.DefaultHeaders.CacheControl.NoCache = true;
            client.DefaultHeaders.CacheControl.NoTransform = true;
            client.DefaultHeaders.CacheControl.AddExtension( "param1" , "1");
            client.DefaultHeaders.CacheControl.AddExtension( "param2" , "2");
            client.DefaultHeaders.CacheControl.AddExtension( "param3" , "3");
            
            client.DefaultHeaders.Add( HeaderNames.Location , "www.azerty1.com" );
            client.DefaultHeaders.Add( HeaderNames.Location , "www.azerty2.com" );
            client.DefaultHeaders.Add( HeaderNames.Location , "www.azerty3.com" );
                
            client.DefaultHeaders.Warning.Add( new WarningHeaderValue( 1, "myagent") );
            client.DefaultHeaders.Warning.Add( new WarningHeaderValue( 2, "myagent") );
            
            foreach( var header in client.DefaultHeaders )
            {
                foreach( var element in header.Value )
                {
                    Console.WriteLine( header.Key + ": " + element );
                }
            }
        }
    }
}