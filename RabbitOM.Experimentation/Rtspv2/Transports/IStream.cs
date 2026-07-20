using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    // so we don't use a pipereader class or an nuget package
    // instead we will implement a special stream
    // with some custom read features and some additionals methods
    // the idea is buffering data before to call a read method and store them into an internal buffer
    // and then we can peek, read, and so on.. theses methods will trigger a true read when the position on this buffer reach the number of bytes reads

    // stream:   [¤¤¤¤¤¤¤¤¤¤$*********¤¤¤¤¤¤¤¤¤¤¤¤¤¤$******]
    // peek:     [¤¤¤¤¤¤¤¤¤¤$*****] trigger a read an save in memory
    // peek:     [¤¤¤¤¤¤¤¤¤¤$*****] returns ¤
    // readByte: [¤¤¤¤¤¤¤¤¤$*****] no trigger a read returns ¤
    // readByte: [¤¤¤¤¤¤¤¤$*****] no trigger a read returns ¤
    // readByte: [¤¤¤¤¤¤¤$*****] no trigger a read returns ¤
    // readByte: [¤¤¤¤¤¤$*****] no trigger a read returns ¤
    // read:     [] no trigger a read and take all remaining returns ¤¤¤¤¤$*****
    // read:     [***¤¤¤¤¤¤¤¤¤¤¤¤¤¤$******] trigger a read returns all
    // read:     [¤¤¤¤¤¤¤¤¤¤¤¤¤¤$******] trigger a read returns all

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
