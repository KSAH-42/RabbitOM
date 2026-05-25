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

    public interface IStream : IDisposable
    {
        bool CanRead { get; }
        
        bool CanWrite { get; }



        
        string ReadLine();

        int PeekByte();

        int ReadByte();

        int Read( byte[] buffer , int offset , int count );

        int WriteByte( byte value );

        int Write( byte[] buffer , int offset , int count );

        void Close();
    }

    //public abstract class IStream : IDisposable
    //{
    //    ~IStream()
    //    {
    //    }



    //    public abstract bool CanRead { get; }
        
    //    public abstract bool CanWrite { get; }



        
    //    public abstract string ReadLine();

    //    public abstract int PeekByte();

    //    public abstract int ReadByte();

    //    public abstract int Read( byte[] buffer , int offset , int count );

    //    public abstract int WriteByte( byte value );

    //    public abstract int Write( byte[] buffer , int offset , int count );

    //    public abstract void Close();

    //    public void Dispose()
    //    {
    //        Dispose( true );
    //        GC.SuppressFinalize( this );
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if ( disposing )
    //        {
    //            Close();
    //        }
    //    }
    //}
}
