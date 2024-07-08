using System.Text;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp.Serialization
{
    /// <summary>
    /// Represent the session description writer
    /// </summary>
    public sealed class SessionDescriptorWriter
    {
        private readonly StringBuilder _builder = new StringBuilder();

        /// <summary>
        /// Write a field
        /// </summary>
        /// <param name="field">the field</param>
        public void WriteField( BaseField field )
        {
            if ( field == null || string.IsNullOrWhiteSpace( field.TypeName ) )
            {
                return;
            }

            if ( ! field.TryValidate() )
            {
                return;
            }

            _builder.AppendFormat( "{0}={1}" , field.TypeName , field.ToString() );
            _builder.AppendLine();
        }

        /// <summary>
        /// Write multiple fields
        /// </summary>
        /// <param name="fields">the fields</param>
        public void WriteFields( IEnumerable<BaseField> fields )
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
