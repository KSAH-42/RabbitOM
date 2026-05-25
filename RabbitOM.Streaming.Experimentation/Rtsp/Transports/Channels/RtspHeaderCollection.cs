using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspHeaderCollection : IEnumerable, IEnumerable<KeyValuePair<string , string[]>>
    {
        // we don't use NameValueCollection here is more slower than the dictionary

        private readonly Dictionary<string,List<string>> _collection = new Dictionary<string, List<string>>( StringComparer.OrdinalIgnoreCase );





        public string this[ string name ]
        {
            get => throw new NotImplementedException();
        }

        public string this[ string name , int index ]
        {
            get => throw new NotImplementedException();
        }







        public int Count
        {
            get => throw new NotImplementedException();
        }

        public IEnumerable<string> AllKeys
        {
            get => throw new NotImplementedException();
        }

        // TODO: need to remove and let the upper layer to parse this header ?
        public long? CSeq
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        // TODO: need to remove and let the upper layer to parse this header ?
        public long? ContentLength
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }






        
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string , string[]>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Contains( string name )
        {
            throw new NotImplementedException();
        }

        public void Add( string name , string value )
        {
            throw new NotImplementedException();
        }

        public string GetValue( string name )
        {
            throw new NotImplementedException();
        }

        public string GetValueAt( string name , int index )
        {
            throw new NotImplementedException();
        }

        public string[] GetValues( string name )
        {
            throw new NotImplementedException();
        }

        public void SetValue( string name , string value )
        {
            throw new NotImplementedException();
        }

        public void Remove( string name )
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool TryAdd( string name , string value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddParse( string input )
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue( string name , out string result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueAt( string name , int index , out string result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetValues( string name , out string[] result )
        {
            throw new NotImplementedException();
        }
    }
}
