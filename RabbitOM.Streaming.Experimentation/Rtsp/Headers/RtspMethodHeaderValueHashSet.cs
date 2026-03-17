using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal class RtspMethodHeaderValueHashSet : RtspHeaderValueCollection<RtspMethod>
    {
        private readonly HashSet<RtspMethod> _collection = new HashSet<RtspMethod>();




        public override object SyncRoot { get => _collection; }

        public override int Count { get => _collection.Count; }

        public override bool IsSynchronized { get => false; }

        public override bool IsReadOnly { get => false; }





        public override void Add( RtspMethod item )
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

        public override bool Contains( RtspMethod item )
        {
            return _collection.Contains( item );
        }

        public override void CopyTo( Array array , int index )
        {
            _collection.CopyTo( array as RtspMethod[] , index );
        }

        public override void CopyTo( RtspMethod[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public override bool Remove( RtspMethod item )
        {
            return _collection.Remove( item );
        }

        public override bool TryAdd( RtspMethod item )
        {
            if ( item == null )
            {
                return false;
            }

            return _collection.Add( item );
        }

        public override bool TryParseWithAdd( string input )
        {
            return RtspMethod.TryParse( input , out var element ) 
                ? TryAdd( element ) 
                : false
                ;
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }
        
        protected override IEnumerator<RtspMethod> BaseGetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
