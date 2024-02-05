using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a reader class used to read field inside a header
    /// </summary>
    internal sealed class RTSPHeaderReader : IDisposable
    {
        private string                       _currentElement          = string.Empty;

        private string                       _currentElementValue     = string.Empty;

        private RTSPOperator                 _operator                = RTSPOperator.Colon;

        private bool                         _includeQuotes           = false;

        private readonly ISet<string>        _elements                = null;

        private readonly ISet<string>        _sequences               = null;

        private readonly IEnumerator<string> _enumerator              = null;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elements">the elements</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPHeaderReader( IEnumerable<string> elements )
        {
            if ( elements == null )
            {
                throw new ArgumentNullException( nameof( elements ) );
            }

            _elements = new HashSet<string>( elements );
            _sequences = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
            _enumerator = _elements.GetEnumerator();
        }






        /// <summary>
        /// Gets / Sets the operator
        /// </summary>
        public RTSPOperator Operator
        {
            get => _operator;
            set => _operator = value;
        }

        /// <summary>
        /// Gets / Sets the include quotes
        /// </summary>
        public bool IncludeQuotes
        {
            get => _includeQuotes;
            set => _includeQuotes = value;
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int NumberOfElements
        {
            get => _elements.Count;
        }

        /// <summary>
        /// Check if the current instance hold some elements
        /// </summary>
        public bool HasElements
        {
            get => _elements.Count > 0;
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsAddressElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Address );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsRtpAvpElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.RtpAvp );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsRtpAvpUdpElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.RtpAvpUdp );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsRtpAvpTcpElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.RtpAvpTcp );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsPortElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Port );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsInterleavedElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Interleaved );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsUnicastElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Unicast );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsMulticastElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Multicast );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsClientPortElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.ClientPort );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsServerPortElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.ServerPort );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsSSRCElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.SSRC );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsRtpTimeElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.RtpTime );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsSequenceElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Sequence );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsModeElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Mode );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsBasicElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Basic );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsDigestElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Digest );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsUserNameElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.UserName );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsRealmElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Realm );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsNonceElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Nonce );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsResponseElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Response );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsAlgorithmElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Algorithm );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsStaleElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Stale );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsDomainElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Domain );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsOpaqueElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Opaque )
                || CheckElementType( "opque" );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsDestinationElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Destination );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsSourceElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Source );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsUriElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Uri );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsUrlElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Url );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsTimeoutElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Timeout );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsTtlElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.TTL );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsLayersElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Layers );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsChannelElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Channel );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsNptTimeElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Npt );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsTimeElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Time );
        }

        /// <summary>
        /// Check the current element name
        /// </summary>
        public bool IsClockElementType
        {
            get => CheckElementType( RTSPHeaderFieldNames.Clock );
        }







        /// <summary>
        /// Just compare an element name with current element name
        /// </summary>
        /// <param name="elementType">the element type</param>
        /// <returns>returns true for success, otherwise false</returns>
        public bool CheckElementType( string elementType )
        {
            if ( string.IsNullOrWhiteSpace( elementType ) )
            {
                return false;
            }

            return string.Compare( _currentElement , elementType , true ) == 0;
        }

        /// <summary>
        /// Read the next element
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Read()
        {
            _currentElement = string.Empty;
            _currentElementValue = string.Empty;

            _sequences.Clear();

            if ( _enumerator.MoveNext() )
            {
                _currentElement = _enumerator.Current?.Trim() ?? string.Empty;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the current element
        /// </summary>
        /// <returns>returns a value</returns>
        public string GetElement()
        {
            return _currentElement;
        }

        /// <summary>
        /// Gets the element value
        /// </summary>
        /// <returns>returns a value</returns>
        public string GetElementValue()
        {
            return _currentElementValue;
        }

        /// <summary>
        /// Gets the element value
        /// </summary>
        /// <returns>returns a value</returns>
        public int GetElementValueAsInteger()
        {
            return RTSPDataConverter.ConvertToInteger( _currentElementValue );
        }

        /// <summary>
        /// Gets the element value
        /// </summary>
        /// <returns>returns a value</returns>
        public long GetElementValueAsLong()
        {
            return RTSPDataConverter.ConvertToLong( _currentElementValue );
        }

        /// <summary>
        /// Gets the element value
        /// </summary>
        /// <returns>returns a value</returns>
        public byte GetElementValueAsByte()
        {
            return RTSPDataConverter.ConvertToByte( _currentElementValue );
        }

        /// <summary>
        /// Gets a sequence value at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns a value</returns>
        public string GetSequenceValue( int index )
        {
            if ( index < 0 || index >= _sequences.Count )
            {
                return string.Empty;
            }

            return _sequences.ElementAt( index );
        }

        /// <summary>
        /// Gets a sequence value at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns a value</returns>
        public int GetSequenceValueAsInteger( int index )
        {
            return RTSPDataConverter.ConvertToInteger( GetSequenceValue( index ) );
        }

        /// <summary>
        /// Gets a sequence value at the desired index
        /// </summary>
        /// <typeparam name="TEnum">the type of the enum</typeparam>
        /// <param name="index">the index</param>
        /// <returns>returns a value</returns>
        public TEnum GetSequenceValueAsEnum<TEnum>( int index ) where TEnum : struct
        {
            return RTSPDataConverter.ConvertToEnum<TEnum>( GetSequenceValue( index ) );
        }

        /// <summary>
        /// Decode the element 
        /// </summary>
        public void DecodeElementAsBase64()
        {
            _currentElement = RTSPDataConverter.ConvertFromBase64( _enumerator.Current )?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Restore the element 
        /// </summary>
        public void RestoreElement()
        {
            _currentElement = _enumerator.Current?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Parse the element as field
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SplitElementAsField()
        {
            if ( string.IsNullOrWhiteSpace( _currentElement ) )
            {
                return false;
            }

            if ( _currentElement.IndexOf( (char) _operator ) <= 0 )
            {
                return false;
            }

            var tokens = _currentElement.Split( new char[] { (char) _operator } );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            _currentElement = tokens[0].Trim();
            _currentElementValue = tokens.Length > 1 ? string.Join( ( (char) _operator ).ToString() , tokens , 1 , tokens.Length - 1 )?.Trim() ?? string.Empty : string.Empty;

            if ( _includeQuotes )
            {
                _currentElementValue = _currentElementValue.Replace( "\'" , string.Empty );
                _currentElementValue = _currentElementValue.Replace( "\"" , string.Empty );
            }

            return true;
        }

        /// <summary>
        /// Parse the element as sequence
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SplitElementAsSequence()
        {
            return SplitElementAsSequence( RTSPSeparator.Slash );
        }

        /// <summary>
        /// Parse the element as sequence
        /// </summary>
        /// <param name="separator">the separator</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SplitElementAsSequence( RTSPSeparator separator )
        {
            _sequences.Clear();

            if ( string.IsNullOrWhiteSpace( _currentElement ) )
            {
                return false;
            }

            if ( _currentElement.IndexOf( (char) separator ) < 0 )
            {
                return false;
            }

            var tokens = _currentElement.Split( new char[] { (char) separator } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            foreach ( var token in tokens )
            {
                var element = token?.Trim() ?? string.Empty;

                if ( string.IsNullOrWhiteSpace( element ) )
                {
                    continue;
                }

                _sequences.Add( element );
            }

            return true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _enumerator.Dispose();
        }
    }
}
