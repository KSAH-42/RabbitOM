namespace RabbitOM.Streaming.Net.Rtp.H266
{
    public class H266MediaElement : RtpMediaElement
    {
        public H266MediaElement( byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] vps , byte[] buffer ) : base ( buffer )
        {
            // TODO: refactor futur duplicated code with H265 media element class ?

            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
            VPS = vps;
        }

        public byte[] StartCodePrefix { get; }

        public byte[] PPS { get; }

        public byte[] SPS { get; }

        public byte[] VPS { get; }
    }
}