using System;
using System.Collections;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: and extension method for methods to accept string instead of RtspHeaderValue when adding value on the collection
    public interface IHeaderCollection : ICollection , IReadOnlyHeaderCollection
    {
        void Add( string name , RtspHeaderValue value ); // should be a virtual method
        
        // TODO: TO AVOID REMOVING CSeq include void Clear() methods

        bool Remove( string name ); // TODO: should be a virtual method TO AVOID REMOVING CSeq
        bool RemoveAt( string name , int index ); // TODO: should be a virtual method TO AVOID REMOVING CSeq


        bool TryAdd( string name , RtspHeaderValue value ); // TODO: should be a virtual method
        bool TryParseWithAdd( string input ); // TODO: should be a virtual method
    }
}
