this assembly is used for experimentation for finding different approachs of some existing implementation.
If the implementation will be enougth, it will be moved to the main assembly.

some modifications on headers will comes and can potentially changed entirely the existing code.

it should not be considered as the final implementation but closer to the final implementation.

so do not used theses classes, until it was moved to the main assembly.


internal class Program
{
    static void Main( string[] args )
    {
        using ( var client = new RabbitOM.Net.Rtsp.RtspClient() )
        {
    	    var requestHeaders = new RequestRtspHeader();

   	        requestHeaders.Accept = new AcceptRtspHeader();
   	        requestHeaders.Accept.Mimes.Add( new StringWithQuality("sdp") );
   	        requestHeaders.Accept.Mimes.Add( new StringWithQuality("application/sdp") );
   	        requestHeaders.Accept.Mimes.Add( new StringWithQuality("text") );
   	        requestHeaders.Accept.Mimes.Add( new StringWithQuality("text/sdp") );
   	       
   	        requestHeaders.AcceptEncoding = new AcceptEncodingRtspHeader();
   	        requestHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("plain") );
   	        requestHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("zip") );
   	        requestHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("tar") );
   	        requestHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("br") );
   	        requestHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("newWinZip") );

   	        requestHeaders.AcceptLanguage = new AcceptLanguageRtspHeader();
   	        requestHeaders.AcceptLanguage.Cultures.Add( new StringWithQuality("fr-FR") );
   	        requestHeaders.AcceptLanguage.Cultures.Add( new StringWithQuality("en-GB") );
   	        requestHeaders.AcceptLanguage.Cultures.Add( new StringWithQuality("en-US") );
   	           	        
   	        client.DefaultHeaders.AddRange( requestHeaders.ToList() );

   	        // client.DefaultHeaders => collection that accept only RtspHeader, extensions method will be added for add( string name , string value ); the collection must expose virtual method to deny header if it's for request or response usage, etc...
        }
    }
}
