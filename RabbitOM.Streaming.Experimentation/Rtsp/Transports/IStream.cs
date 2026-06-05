using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    // so we don't use a pipereader class, instead we use a special stream
    // with some custom read optimizations and additional methods
    // the idea is buffering data with before to call a read method
    // increase the position, but only in some cases
    // for instance any call on read method will fill an internal large buffer
    // only if the position has reach the limit and then we trigger a true read on the socket
    // the peekmethod can be also introduce

    //      ^
    //     /|\
    //    / | \
    //   /  |  \
    //  /   *   \
    // /_________\ 

    // here no refactoring of ascii art here, even if it's horrible, but here

    // this interface has been used instead of using the base .net class Stream
    // the reasons comes that the Stream class expose seeks methods that
    // can introduce confusion due that a network stream can support seek, you know because it's realtime data
    // and throw exception on unsupported methods is for me not a good option

    // that's due when i write a plugin framework, i have expose many features
    // and after that i have learn that we need to reduce the number of possibilities,
    // features should be hidden and protected against bad usages, the problem is not the abstract Stream class... 

    public interface IStream : IDisposable
    {
        int Peek();

        int ReadByte();

        int Read( byte[] buffer , int offset , int count );

        void WriteByte( byte value );

        void Write( byte[] buffer , int offset , int count );

        void Flush();

        void Discard();

        void Close();
    }
}
