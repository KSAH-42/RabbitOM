namespace RabbitOM.Streaming.Net.Rtp.H266
{
    public class H266FrameMediaElement : RtpMediaElement
    {
        public H266FrameMediaElement( byte[] buffer , byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] vps ) : base ( buffer )
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