using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public abstract class RtspHeaderValueCollection<T> : IEnumerable, IEnumerable<T>, ICollection, ICollection<T>, IReadOnlyCollection<T>
        where T : class
    {
        public abstract object SyncRoot { get; }
        public abstract int Count { get; }
        public abstract bool IsSynchronized { get; }
        public abstract bool IsReadOnly { get; }






        public abstract void Add( T item );
        public abstract void Clear();
        public abstract bool Contains( T item );
        public abstract void CopyTo( Array array , int index );
        public abstract void CopyTo( T[] array , int arrayIndex );
        public abstract bool Remove( T item );
        public abstract bool TryAdd( T item );
        public abstract bool TryParseWithAdd( string input ); 
        public abstract override string ToString();
        public IEnumerator GetEnumerator() => BaseGetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => BaseGetEnumerator();
        protected abstract IEnumerator<T> BaseGetEnumerator();
    }
}
