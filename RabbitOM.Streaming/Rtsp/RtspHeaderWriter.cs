using System;
using System.Text;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header writer
    /// </summary>
    internal sealed class RtspHeaderWriter
    {
        private RtspSeparator            _separator      = RtspSeparator.Comma;

        private RtspOperator             _operator       = RtspOperator.Equality;

        private bool                     _includeQuotes  = false;

        private readonly StringBuilder   _builder        = new StringBuilder();






        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        public RtspHeaderWriter()
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="separator">the separator</param>
        public RtspHeaderWriter( RtspSeparator separator )
        {
            Separator = separator;
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="separator">the separator</param>
        /// <param name="operator">the operator</param>
        public RtspHeaderWriter( RtspSeparator separator , RtspOperator @operator )
        {
            Separator = separator;
            Operator  = @operator;
        }






        /// <summary>
        /// Gets / Sets the separator type
        /// </summary>
        public RtspSeparator Separator
        {
            get => _separator;
            set => _separator = value;
        }

        /// <summary>
        /// Gets / Sets the operator type
        /// </summary>
        public RtspOperator Operator
        {
            get => _operator;
            set => _operator = value;
        }

        /// <summary>
        /// Gets / Sets the include quotes usage status
        /// </summary>
        public bool IncludeQuotes
        {
            get => _includeQuotes;
            set => _includeQuotes = value;
        }

        /// <summary>
        /// Gets the output length
        /// </summary>
        public int Length
        {
            get => _builder.Length;
        }

        /// <summary>
        /// Check if the output length is empty
        /// </summary>
        public bool IsEmpty
        {
            get => _builder.Length <= 0;
        }

        /// <summary>
        /// Gets the output
        /// </summary>
        public string Output
        {
            get => ToString();
        }






        /// <summary>
        /// Check if an element can be written
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanWrite( string value )
        {
            return ! string.IsNullOrWhiteSpace( value );
        }

        /// <summary>
        /// Check if an element can be written
        /// </summary>
        /// <param name="parameterName">the parameter name</param>
        /// <param name="parameterValue">the parameter value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanWrite( string parameterName , string parameterValue )
        {
            return ! string.IsNullOrWhiteSpace( parameterName )
                && ! string.IsNullOrWhiteSpace( parameterValue );
        }

        /// <summary>
        /// Write a separator
        /// </summary>
        public void WriteSeparator()
        {
            WriteSeparator( _separator );
        }

        /// <summary>
        /// Write a separator
        /// </summary>
        /// <param name="separator">the separator</param>
        public void WriteSeparator( RtspSeparator separator )
        {
            Write( (char) separator );
        }

        /// <summary>
        /// Write a separator
        /// </summary>
        public void WriteOperator()
        {
            WriteOperator( _operator );
        }

        /// <summary>
        /// Write a separator
        /// </summary>
        /// <param name="operator">the operator</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public void WriteOperator( RtspOperator @operator )
        {
            Write( (char) @operator );
        }

        /// <summary>
        /// Write a space
        /// </summary>
        public void WriteSpace()
        {
            Write( " " );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( bool value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( char value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( sbyte value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( byte value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( short value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( ushort value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( int value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( uint value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( long value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( ulong value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( decimal value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( float value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( double value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( DateTime value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( TimeSpan value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( Guid value )
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <typeparam name="TEnum">the type of enum</typeparam>
        /// <param name="value">the value</param>
        public void Write<TEnum>( TEnum value ) where TEnum : struct
        {
            Write( value.ToString() );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void Write( string value )
        {
            InternalWrite( value );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteAsBase64( string value )
        {
            InternalWriteAsBase64( value );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , bool fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , char fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , sbyte fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , byte fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , short fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , ushort fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , int fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , uint fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , long fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , ulong fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , decimal fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , float fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , double fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , DateTime fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , TimeSpan fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , Guid fieldValue )
        {
            WriteField( fieldName , fieldValue.ToString() );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteField( string fieldName , string fieldValue )
        {
            InternalWrite( InternalCreate( fieldName , fieldValue ) );
        }

        /// <summary>
        /// Write a field
        /// </summary>
        /// <param name="fieldName">the parameter name</param>
        /// <param name="fieldValue">the parameter value </param>
        public void WriteFieldAsBase64( string fieldName , string fieldValue )
        {
            InternalWriteAsBase64( InternalCreate( fieldName , fieldValue ) );
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="value">the value</param>
        private void InternalWrite( string value )
        {
            _builder.Append( value ?? string.Empty );
        }

        /// <summary>
        /// Write an element
        /// </summary>
        /// <param name="value">the value</param>
        private void InternalWriteAsBase64( string value )
        {
            _builder.Append( RtspDataConverter.ConvertToBase64( value ) );
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="parameterName">the parameter name</param>
        /// <param name="parameterValue">the parameter value </param>
        private string InternalCreate( string parameterName , string parameterValue )
        {
            var builder = new StringBuilder();

            builder.Append( parameterName ?? string.Empty );
            builder.Append( (char) _operator );

            if ( _includeQuotes )
            {
                builder.Append( "\"" );
            }

            builder.Append( parameterValue ?? string.Empty );

            if ( _includeQuotes )
            {
                builder.Append( "\"" );
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return _builder.ToString().Trim();
        }
    }
}
