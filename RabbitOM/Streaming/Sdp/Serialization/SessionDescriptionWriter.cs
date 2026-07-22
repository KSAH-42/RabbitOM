using System.Text;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp.Serialization
{
    public sealed class SessionDescriptionWriter
    {
        private readonly StringBuilder _builder = new StringBuilder();

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

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
