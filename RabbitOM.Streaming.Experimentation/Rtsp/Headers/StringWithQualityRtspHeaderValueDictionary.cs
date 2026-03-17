using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal class StringWithQualityRtspHeaderValueDictionary : RtspHeaderValueCollection<StringWithQuality>
    {
        private readonly Dictionary<string,StringWithQuality> _collection;

        private readonly Func<StringWithQuality,bool> _validator;





        public StringWithQualityRtspHeaderValueDictionary()
            : this ( _ => true )
        {
        }

        public StringWithQualityRtspHeaderValueDictionary( Func<StringWithQuality,bool> validator )
        {
            _validator = validator ?? throw new ArgumentNullException( nameof( validator ) );

            _collection = new Dictionary<string, StringWithQuality>( StringComparer.OrdinalIgnoreCase );
        }





        public override object SyncRoot { get => _collection; }

        public override int Count { get => _collection.Count; }

        public override bool IsSynchronized { get => false; }

        public override bool IsReadOnly { get => false; }





        public override void Add( StringWithQuality item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            if ( StringWithQuality.IsNullOrEmpty( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }

            if ( ! _validator.Invoke( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }

            _collection.Add( item.Value , item );
        }

        public override void Clear()
        {
            _collection.Clear();
        }

        public override bool Contains( StringWithQuality item )
        {
            return _collection.ContainsValue( item );
        }

        public override void CopyTo( Array array , int index )
        {
            _collection.Values.CopyTo( array as StringWithQuality[] , index );
        }

        public override void CopyTo( StringWithQuality[] array , int arrayIndex )
        {
            _collection.Values.CopyTo( array , arrayIndex );
        }

        public override bool Remove( StringWithQuality item )
        {
            if ( StringWithQuality.IsNullOrEmpty( item ) || ! _collection.ContainsKey( item.Value ) )
            {
                return false;
            }

            return _collection.Remove( item.Value );
        }

        public override bool TryAdd( StringWithQuality item )
        {
            if ( StringWithQuality.IsNullOrEmpty( item ) || ! _validator.Invoke( item ) )
            {
                return false;
            }

            if ( _collection.ContainsKey( item.Value ) )
            {
                return false;
            }

            _collection.Add( item.Value , item );

            return true;
        }

        public override bool TryParseWithAdd( string input )
        {
            return StringWithQuality.TryParse( input , out var element ) 
                ? TryAdd( element ) 
                : false
                ;
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }
        
        protected override IEnumerator<StringWithQuality> BaseGetEnumerator()
        {
            return _collection.Values.GetEnumerator();
        }
    }
}
