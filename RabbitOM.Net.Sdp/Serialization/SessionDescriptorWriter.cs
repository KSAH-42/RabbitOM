using System.Text;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp.Serialization
{
    /// <summary>
    /// Represent the session description writer
    /// </summary>
    public sealed class SessionDescriptorWriter
    {
        private readonly StringBuilder _builder = new StringBuilder();

        /// <summary>
        /// Gets the output
        /// </summary>
        public string Output
        {
            get => _builder.ToString();
        }

        /// <summary>
        /// Write a field
        /// </summary>
        /// <param name="field">the field</param>
        public void WriteField( BaseField field )
        {
            if ( field == null || string.IsNullOrWhiteSpace(field.TypeName) )
            {
                return;
            }
            
            if ( ! field.TryValidate() )
            {
                return;
            }

            _builder.Append( field.TypeName );
            _builder.Append( "=" );
            _builder.Append( field.ToString() );
            _builder.AppendLine();
        }

        /// <summary>
        /// Write multiple fields
        /// </summary>
        /// <typeparam name="TField">the type of field</typeparam>
        /// <param name="fields">the fields</param>
        public void WriteFields<TField>( IEnumerable<TField> fields )
            where TField : BaseField
        {
            if ( fields != null )
            {
                foreach ( var field in fields )
                {
                    WriteField( field );
                }
            }
        }

        /// <summary>
        /// Gets the output
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
