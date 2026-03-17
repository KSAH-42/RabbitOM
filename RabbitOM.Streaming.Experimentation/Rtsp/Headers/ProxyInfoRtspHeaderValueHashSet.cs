using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal class ProxyInfoRtspHeaderValueHashSet : RtspHeaderValueCollection<ProxyInfo>
    {
        private readonly HashSet<ProxyInfo> _collection = new HashSet<ProxyInfo>();




        public override object SyncRoot { get => _collection; }

        public override int Count { get => _collection.Count; }

        public override bool IsSynchronized { get => false; }

        public override bool IsReadOnly { get => false; }





        public override void Add( ProxyInfo item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            if ( ! _collection.Add( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }
        }

        public override void Clear()
        {
            _collection.Clear();
        }

        public override bool Contains( ProxyInfo item )
        {
            return _collection.Contains( item );
        }

        public override void CopyTo( Array array , int index )
        {
            _collection.CopyTo( array as ProxyInfo[] , index );
        }

        public override void CopyTo( ProxyInfo[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public override bool Remove( ProxyInfo item )
        {
            return _collection.Remove( item );
        }

        public override bool TryAdd( ProxyInfo item )
        {
            if ( item == null )
            {
                return false;
            }

            return _collection.Add( item );
        }

        public override bool TryParseWithAdd( string input )
        {
            return ProxyInfo.TryParse( input , out var element ) 
                ? TryAdd( element ) 
                : false
                ;
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }
        
        protected override IEnumerator<ProxyInfo> BaseGetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
