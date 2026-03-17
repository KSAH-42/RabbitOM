using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    internal class StringRtspHeaderValueHashSet : RtspHeaderValueCollection<string>
    {
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;



        private readonly HashSet<string> _collection;

        private readonly Func<string,bool> _validator;





        public StringRtspHeaderValueHashSet()
            : this ( _ => true )
        {
        }

        public StringRtspHeaderValueHashSet( Func<string,bool> validator )
        {
            _validator = validator ?? throw new ArgumentNullException( nameof( validator ) );

            _collection = new HashSet<string>();
        }






        public static StringRtspHeaderValueHashSet NewStringDirectiveCollection()
        {
            return new StringRtspHeaderValueHashSet( RtspHeaderValueValidator.TryValidateDirective );
        }






        public override object SyncRoot { get => _collection; }

        public override int Count { get => _collection.Count; }

        public override bool IsSynchronized { get => false; }

        public override bool IsReadOnly { get => false; }





        public override void Add( string item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            if ( string.IsNullOrEmpty( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }

            if ( ! _validator.Invoke( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }

            if ( ! _collection.Add( ValueAdapter.Adapt( item ) ) )
            {
                throw new ArgumentException( nameof( item ) );
            }
        }

        public override void Clear()
        {
            _collection.Clear();
        }

        public override bool Contains( string item )
        {
            return _collection.Contains( item );
        }

        public override void CopyTo( Array array , int index )
        {
            _collection.CopyTo( array as string[] , index );
        }

        public override void CopyTo( string[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public override bool Remove( string item )
        {
            return _collection.Remove( ValueAdapter.Adapt( item ) );
        }

        public override bool TryAdd( string item )
        {
            if ( string.IsNullOrEmpty( item ) || ! _validator.Invoke( item ) )
            {
                return false;
            }

            return _collection.Add( ValueAdapter.Adapt( item ) );
        }

        public override bool TryParseWithAdd( string input )
        {
            return TryAdd( input );
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }
        
        protected override IEnumerator<string> BaseGetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
