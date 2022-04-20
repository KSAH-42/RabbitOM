using System;

namespace RabbitOM.Net.Sdp.Serialization
{
    /// <summary>
    /// Represent a tolerant sdp serializer
    /// </summary>
    /// <remarks>
    ///    <para>According to the global authorities, the serialization mecanism use a certain order: please follow this link to get more details <see href="https://tools.ietf.org/html/rfc4566#page-39"/></para>
    /// </remarks>
    public static class SessionDescriptorSerializer
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="sdp">the sdp</param>
        /// <returns>returns string</returns>
        public static string Serialize( SessionDescriptor sdp )
        {
            if ( sdp == null )
            {
                return string.Empty;
            }

            var writer = new SessionDescriptorWriter();

            writer.WriteField( sdp.Version );
            writer.WriteField( sdp.Origin );
            writer.WriteField( sdp.SessionName );
            writer.WriteField( sdp.SessionInformation );
            writer.WriteField( sdp.Uri );
            writer.WriteFields( sdp.Emails );
            writer.WriteFields( sdp.Phones );
            writer.WriteField( sdp.Connection );
            writer.WriteFields( sdp.Bandwiths );
            writer.WriteFields( sdp.Times );
            writer.WriteFields( sdp.Repeats );
            writer.WriteField( sdp.TimeZone );
            writer.WriteField( sdp.Encryption );
            writer.WriteFields( sdp.Attributes );

            foreach ( var mediaDescription in sdp.MediaDescriptions )
            {
                writer.WriteField( mediaDescription );
                writer.WriteField( mediaDescription.Connection );
                writer.WriteFields( mediaDescription.Bandwiths );
                writer.WriteField( mediaDescription.Encryption );
                writer.WriteFields( mediaDescription.Attributes );
            }

            return writer.Output ?? string.Empty;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="input">the input value</param>
        /// <returns>returns an instance</returns>
        public static SessionDescriptor Deserialize( string input )
        {
            using ( var reader = new SessionDescriptorReader( input ) )
            {
                var builder = new SessionDescriptorBuilder();

                while ( reader.Read() )
                {
                    if ( reader.IsVersionField )
                    {
                        builder.SetVersion( reader.CurrentValue );
                    }

                    if ( reader.IsOriginField )
                    {
                        builder.SetOrigin( reader.CurrentValue );
                    }

                    if ( reader.IsSessionNameField )
                    {
                        builder.SetSessionName( reader.CurrentValue );
                    }

                    if ( reader.IsSessionInformationField )
                    {
                        builder.SetSessionInformation( reader.CurrentValue );
                    }

                    if ( reader.IsUriField )
                    {
                        builder.SetUri( reader.CurrentValue );
                    }

                    if ( reader.IsEmailField )
                    {
                        builder.AddEmail( reader.CurrentValue );
                    }

                    if ( reader.IsPhoneField )
                    {
                        builder.AddPhone( reader.CurrentValue );
                    }

                    if ( reader.IsConnectionField )
                    {
                        builder.SetConnection( reader.CurrentValue );
                    }

                    if ( reader.IsBandwithField )
                    {
                        builder.AddBandwith( reader.CurrentValue );
                    }

                    if ( reader.IsTimeField )
                    {
                        builder.AddTime( reader.CurrentValue );
                    }

                    if ( reader.IsRepeatField )
                    {
                        builder.AddRepeat( reader.CurrentValue );
                    }

                    if ( reader.IsTimeZoneField )
                    {
                        builder.SetTimeZone( reader.CurrentValue );
                    }

                    if ( reader.IsEncryptionField )
                    {
                        builder.SetEncryption( reader.CurrentValue );
                    }

                    if ( reader.IsAttributeField )
                    {
                        builder.AddAttribute( reader.CurrentValue );
                    }

                    if ( reader.IsUnderMediaSection )
                    {
                        do
                        {
                            if ( reader.IsMediaDescriptionField )
                            {
                                builder.CreateMediaDescription( reader.CurrentValue );
                            }

                            if ( reader.IsConnectionField )
                            {
                                builder.SetMediaConnection( reader.CurrentValue );
                            }

                            if ( reader.IsEncryptionField )
                            {
                                builder.SetMediaEncryption( reader.CurrentValue );
                            }

                            if ( reader.IsBandwithField )
                            {
                                builder.AddMediaBandwith( reader.CurrentValue );
                            }

                            if ( reader.IsAttributeField )
                            {
                                builder.AddMediaAttribute( reader.CurrentValue );
                            }
                        }
                        while ( reader.Read() && reader.IsUnderMediaSection );
                    }
                }

                return builder.Build();
            }
        }
    }
}
