using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
	public sealed class JpegFragmentQueue : IEnumerable , IEnumerable<JpegFragment> , IReadOnlyCollection<JpegFragment>
	{
		private readonly Queue<JpegFragment> _collection;





		public JpegFragmentQueue()
		{
			_collection = new Queue<JpegFragment>();
		}

		public JpegFragmentQueue( int capacity )
		{
			_collection = new Queue<JpegFragment>( capacity );
		}

		public JpegFragmentQueue( IEnumerable<JpegFragment> collection )
		{
			if ( collection == null )
			{
				throw new ArgumentNullException( nameof( collection ) );
			}

			_collection = new Queue<JpegFragment>();  // avoid a filter using linq and pass the result on the constuctor for performance reason, look using reflector

			foreach ( var element in collection )
			{
				_collection.Enqueue( element ?? throw new ArgumentNullException( "Bad element" , nameof( collection ) ) );
			}
		}






		public int Count
		{
			get => _collection.Count;
		}

		public bool IsEmpty
		{
			get => _collection.Count == 0;
		}







		IEnumerator IEnumerable.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public IEnumerator<JpegFragment> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public bool Any()
		{
			return _collection.Count > 0;
		}

		public bool Contains( JpegFragment fragment )
		{
			return _collection.Contains( fragment );
		}

		public void Clear()
		{
			_collection.Clear();
		}

		public JpegFragment[] ToArray()
		{
			return _collection.ToArray();
		}

		public void Enqueue( JpegFragment fragment )
		{
			_collection.Enqueue( fragment ?? throw new ArgumentNullException( nameof( fragment ) ) );
		}

		public JpegFragment Dequeue()
		{
			return _collection.Dequeue() ?? throw new InvalidOperationException();
		}

		public JpegFragment Peek()
		{
			return _collection.Peek() ?? throw new InvalidOperationException();
		}

		public bool TryEnqueue( JpegFragment fragment )
		{
			if ( fragment == null )
			{
				return false;
			}

			_collection.Enqueue( fragment );

			return true;
		}

		public bool TryDequeue( out JpegFragment result )
		{
			result = _collection.Count > 0 ? _collection.Dequeue() : null;

			return result != null;
		}

		public bool TryPeek( out JpegFragment result )
		{
			result = _collection.Count > 0 ? _collection.Peek() : null ;
			
			return result != null;
		}
	}
}