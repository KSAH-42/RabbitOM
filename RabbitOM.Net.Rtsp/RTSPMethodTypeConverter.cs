using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent type converter class
    /// </summary>
    public static class RTSPMethodTypeConverter
    {
        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="methodType">the method type</param>
        /// <returns>returns a string value</returns>
        public static string Convert( RTSPMethodType methodType )
        {
            switch ( methodType )
            {
                case RTSPMethodType.Announce:
                    return RTSPMethodTypeNames.ANNOUNCE;

                case RTSPMethodType.Describe:
                    return RTSPMethodTypeNames.DESCRIBE;

                case RTSPMethodType.GetParameter:
                    return RTSPMethodTypeNames.GET_PARAMETER;

                case RTSPMethodType.Options:
                    return RTSPMethodTypeNames.OPTIONS;

                case RTSPMethodType.Pause:
                    return RTSPMethodTypeNames.PAUSE;

                case RTSPMethodType.Play:
                    return RTSPMethodTypeNames.PLAY;

                case RTSPMethodType.Record:
                    return RTSPMethodTypeNames.RECORD;

                case RTSPMethodType.Redirect:
                    return RTSPMethodTypeNames.REDIRECT;

                case RTSPMethodType.Setup:
                    return RTSPMethodTypeNames.SETUP;

                case RTSPMethodType.SetParameter:
                    return RTSPMethodTypeNames.SET_PARAMETER;

                case RTSPMethodType.TearDown:
                    return RTSPMethodTypeNames.TEARDOWN;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="methodType">the method type</param>
        /// <returns>returns a string value</returns>
        public static RTSPMethodType Convert( string methodType )
        {
            if ( string.IsNullOrWhiteSpace( methodType ) )
            {
                return RTSPMethodType.UnDefined;
            }

            var method     = methodType.Trim();
            var ignoreCase = true;

            if ( string.Compare( RTSPMethodTypeNames.ANNOUNCE , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Announce;
            }

            if ( string.Compare( RTSPMethodTypeNames.DESCRIBE , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Describe;
            }

            if ( string.Compare( RTSPMethodTypeNames.GET_PARAMETER , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.GetParameter;
            }

            if ( string.Compare( RTSPMethodTypeNames.OPTIONS , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Options;
            }

            if ( string.Compare( RTSPMethodTypeNames.PAUSE , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Pause;
            }

            if ( string.Compare( RTSPMethodTypeNames.PLAY , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Play;
            }

            if ( string.Compare( RTSPMethodTypeNames.RECORD , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Record;
            }

            if ( string.Compare( RTSPMethodTypeNames.REDIRECT , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Redirect;
            }

            if ( string.Compare( RTSPMethodTypeNames.SETUP , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.Setup;
            }

            if ( string.Compare( RTSPMethodTypeNames.SET_PARAMETER , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.SetParameter;
            }

            if ( string.Compare( RTSPMethodTypeNames.TEARDOWN , method , ignoreCase ) == 0 )
            {
                return RTSPMethodType.TearDown;
            }

            return RTSPMethodType.UnDefined;
        }
    }
}
