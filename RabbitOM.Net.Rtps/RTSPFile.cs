using System;
using System.IO;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a RTSP file
    /// </summary>
    public sealed class RTSPFile : IDisposable
    {
        private readonly object    _lock     = new object();

        private string             _name     = string.Empty;

        private FileStream         _stream   = null;





        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPFile()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">the file name</param>
        public RTSPFile( string fileName )
        {
            Open( fileName );
        }





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name
        {
            get
            {
                lock ( _lock )
                {
                    return _name;
                }
            }
        }

        /// <summary>
        /// Check if the stream has been opened
        /// </summary>
        public bool IsOpened
        {
            get
            {
                lock ( _lock )
                {
                    return _stream != null;
                }
            }
        }

        /// <summary>
        /// Gets the length
        /// </summary>
        public long Length
        {
            get
            {
                try
                {
                    lock ( _lock )
                    {
                        return _stream?.Length ?? 0;
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return -1;
            }
        }

        /// <summary>
        /// Check if we can perform read operations
        /// </summary>
        public bool CanRead
        {
            get
            {
                try
                {
                    lock ( _lock )
                    {
                        return _stream?.CanRead ?? false;
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return false;
            }
        }

        /// <summary>
        /// Check if we can perform write operations
        /// </summary>
        public bool CanWrite
        {
            get
            {
                try
                {
                    lock ( _lock )
                    {
                        return _stream?.CanWrite ?? false;
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return false;
            }
        }





        /// <summary>
        /// Open the file
        /// </summary>
        /// <param name="fileName">the file name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( string fileName )
        {
            return Open( fileName , FileAccess.ReadWrite );
        }

        /// <summary>
        /// Open the file
        /// </summary>
        /// <param name="fileName">the file name</param>
        /// <param name="access">the access mode</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( string fileName , FileAccess access )
        {
            if ( string.IsNullOrWhiteSpace( fileName ) )
            {
                return false;
            }

            try
            {
                lock ( _lock )
                {
                    if ( _stream != null )
                    {
                        return false;
                    }

                    _stream = File.Open( fileName , FileMode.OpenOrCreate | FileMode.Truncate , access );

                    _name = fileName;

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Close the stream
        /// </summary>
        public void Close()
        {
            try
            {
                lock ( _lock )
                {
                    _stream.Close();
                    _stream = null;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }

        /// <summary>
        /// Write 
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns for a success, otherwise false</returns>
        public bool Write( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return false;
            }

            if ( count <= 0 || count > buffer.Length )
            {
                return false;
            }

            try
            {
                lock ( _lock )
                {
                    if ( _stream != null )
                    {
                        _stream.Write( buffer , offset , count );

                        return true;
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read 
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns the number of bytes read</returns>
        public int Read( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return -1;
            }

            if ( count <= 0 || count > buffer.Length )
            {
                return 0;
            }

            try
            {
                lock ( _lock )
                {
                    if ( _stream != null )
                    {
                        return _stream.Read( buffer , offset , count );
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return -1;
        }

        /// <summary>
        /// Flush pending bytes
        /// </summary>
        public void Flush()
        {
            try
            {
                lock ( _lock )
                {
                    if ( _stream != null )
                    {
                        _stream.Flush();
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }





        /// <summary>
        /// Handle the exception
        /// </summary>
        /// <param name="exception">the exception</param>
        private void OnError( Exception exception )
        {
            if ( exception == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
