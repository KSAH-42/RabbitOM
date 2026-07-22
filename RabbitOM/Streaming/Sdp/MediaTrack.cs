using System;

namespace RabbitOM.Streaming.Sdp
{
    public sealed class MediaTrack
    {
        private readonly Guid _uniqueId;

        private string _controlUri = string.Empty;

        private string _mimeType = string.Empty;

        private string _address = string.Empty;

        private int _port = 0;

        private readonly RtpMapAttributeValue _rtpMap = new RtpMapAttributeValue();

        private readonly FormatAttributeValue _format = new FormatAttributeValue();




        public MediaTrack() : this(Guid.NewGuid())
        {
        }

        public MediaTrack(Guid uniqueId)
        {
            _uniqueId = uniqueId;
        }



        public Guid UniqueId
        {
            get => _uniqueId;
        }

        public string ControlUri
        {
            get => _controlUri;
            set => _controlUri = DataConverter.Filter(value);
        }

        public string MimeType
        {
            get => _mimeType;
            set => _mimeType = DataConverter.Filter(value);
        }

        public string Address
        {
            get => _address;
            set => _address = DataConverter.Filter(value);
        }

        public int Port
        {
            get => _port;
            set => _port = value;
        }

        public RtpMapAttributeValue RtpMap
        {
            get => _rtpMap;
        }

        public FormatAttributeValue Format
        {
            get => _format;
        }



        public bool Validate()
        {
            if (_uniqueId == Guid.Empty)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_controlUri) || string.IsNullOrWhiteSpace(_address))
            {
                return false;
            }

            if (_port <= 0)
            {
                return false;
            }

            return _rtpMap.TryValidate();
        }

        public static MediaTrack Create()
        {
            return Create(Guid.NewGuid());
        }

        public static MediaTrack Create(Guid identifier)
        {
            return new MediaTrack(identifier);
        }
    }
}
