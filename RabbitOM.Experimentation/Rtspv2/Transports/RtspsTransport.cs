using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public sealed class RtspsTransport : ITransport
    {
        private readonly SslStream _stream;

        public RtspsTransport( Socket socket , string hostName )
        {
            _stream = new SslStream( new NetworkStream( socket ) , true , OnValidateCertificate , null );
            _stream.AuthenticateAsClient( hostName );
        }

        private static bool OnValidateCertificate( object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return sslPolicyErrors == SslPolicyErrors.None;
        }

        public void Send( byte[] buffer , int offset , int count )
        {
            _stream.Write( buffer , offset , count );
            _stream.Flush();
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            return _stream.Read( buffer , offset , count );
        }

        public void Close()
        {
            _stream.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
