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
            client.DefaultHeaders.Accept = new AcceptRtspHeader();
   	        client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("application/text") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
   	        
            client.DefaultHeaders.AcceptEncoding = new AcceptEncodingRtspHeader();
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("plain") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("zip") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("tar") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("br") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("newWinZip") );
            
   	        client.Connect( "rtsp://127.0.0.1/city1.mp4" );

            var request = new RtspClientRequest();

            request.Headers.Accept.Mimes.Add( new StringWithQuality("application/sdp") );
   	        request.Headers.Accept.Mimes.Add( new StringWithQuality("text/sdp") );
            request.Headers.Add( "X-Sdp-Encryption" , "algorithm=abcde;public-key=123123z1zer213==" );

            var response0 = client.Options();

            var response1 = client.Options( request );
            
            var response2 = client.Options( "*" , request );
        }
    }
}
