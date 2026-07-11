using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    //      ^
    //     /|\
    //    / | \
    //   /  |  \
    //  /   *   \
    // /_________\ 

    // this interface has been used instead of using the base .net class Stream
    // the reasons comes that the Stream class expose seeks methods that
    // can introduce confusion due that a network stream can support seek, you know because it's realtime data
    // and throw exception on unsupported methods is for me not a good option

    // that's due when i write a plugin framework, i have expose many features
    // and after that i have learn that we need to reduce the number of possibilities,
    // features should be hidden and protected against bad usages, the problem is not the abstract Stream class... 

    // so we don't use a pipereader class, or an nuget package
    // instead we will implement a special stream
    // with some custom read optimizations and additional methods
    // the idea is buffering data before to call a read method and store them into an internal buffer
    // and then we can peek, read, and so on.. theses methods will trigger a true read when the position on this buffer reach the number of bytes reads

    // stream:   [¤¤¤¤¤¤¤¤¤¤$*********¤¤¤¤¤¤¤¤¤¤¤¤¤¤$******]
    // peek:     [¤¤¤¤¤¤¤¤¤¤$*****] trigger a read an save in memory
    // peek:     [¤¤¤¤¤¤¤¤¤¤$*****] 
    // readByte: [¤¤¤¤¤¤¤¤¤$*****] no trigger a read
    // readByte: [¤¤¤¤¤¤¤¤$*****] no trigger a read
    // readByte: [¤¤¤¤¤¤¤$*****] no trigger a read
    // readByte: [¤¤¤¤¤¤$*****] no trigger a read
    // read:     [] no trigger a read and take all remaining
    // read:     [***¤¤¤¤¤¤¤¤¤¤¤¤¤¤$******] trigger a read
    // read:     [¤¤¤¤¤¤¤¤¤¤¤¤¤¤$******] trigger a read

    public interface IStream : IDisposable
    {
        int Peek();

        int ReadByte();

        int Read( byte[] buffer , int offset , int count );

        void WriteByte( byte value );

        void Write( byte[] buffer , int offset , int count );

        void Flush();

        void Close();
    }
}
