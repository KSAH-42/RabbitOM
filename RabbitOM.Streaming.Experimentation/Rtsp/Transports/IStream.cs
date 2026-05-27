using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    // so we don't use a pipereader class, instead we use a special stream
    // with some custom read optimizations and additional methods
    // the idea is buffering data with before to call a read method
    // increase the position, but only in some cases
    // for instance any read on the stream will use an internal large buffer
    // without to increment the fully position, only when the count bytes use to read,
    // is just smaller than the size of the internal buffer.

    //     / \
    //    / | \
    //   /  |  \
    //  /   *   \
    // /_________\

    // an interface has been used instead of using the base .net class Stream
    // the reasons comes that the Stream class expose seeks methods that
    // can introduce confusion

    public interface IStream : IDisposable
    {
        bool CanRead { get; }

        bool CanWrite { get; }




        string ReadLine();

        int PeekByte();

        int ReadByte();

        int Read( byte[] buffer , int offset , int count );

        void WriteByte( byte value );

        void Write( byte[] buffer , int offset , int count );

        void Close();
    }
}
