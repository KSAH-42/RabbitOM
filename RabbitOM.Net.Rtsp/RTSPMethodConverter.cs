using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent type converter class
    /// </summary>
    public static class RTSPMethodConverter
    {
        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="method">the method type</param>
        /// <returns>returns a string value</returns>
        public static string Convert( RTSPMethod method )
        {
            switch ( method )
            {
                case RTSPMethod.Announce:
                    return RTSPMethodNames.ANNOUNCE;

                case RTSPMethod.Describe:
                    return RTSPMethodNames.DESCRIBE;

                case RTSPMethod.GetParameter:
                    return RTSPMethodNames.GET_PARAMETER;

                case RTSPMethod.Options:
                    return RTSPMethodNames.OPTIONS;

                case RTSPMethod.Pause:
                    return RTSPMethodNames.PAUSE;

                case RTSPMethod.Play:
                    return RTSPMethodNames.PLAY;

                case RTSPMethod.Record:
                    return RTSPMethodNames.RECORD;

                case RTSPMethod.Redirect:
                    return RTSPMethodNames.REDIRECT;

                case RTSPMethod.Setup:
                    return RTSPMethodNames.SETUP;

                case RTSPMethod.SetParameter:
                    return RTSPMethodNames.SET_PARAMETER;

                case RTSPMethod.TearDown:
                    return RTSPMethodNames.TEARDOWN;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="methodName">the method name</param>
        /// <returns>returns a string value</returns>
        public static RTSPMethod Convert( string methodName )
        {
            return Convert( methodName , true );
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="methodName">the method name</param>
        /// <param name="ignoreCase">the ignore case</param>
        /// <returns>returns a string value</returns>
        public static RTSPMethod Convert( string methodName , bool ignoreCase )
        {
            if ( string.IsNullOrWhiteSpace( methodName ) )
            {
                return RTSPMethod.UnDefined;
            }

            var method = methodName.Trim();

            if ( string.Compare( RTSPMethodNames.ANNOUNCE , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Announce;
            }

            if ( string.Compare( RTSPMethodNames.DESCRIBE , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Describe;
            }

            if ( string.Compare( RTSPMethodNames.GET_PARAMETER , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.GetParameter;
            }

            if ( string.Compare( RTSPMethodNames.OPTIONS , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Options;
            }

            if ( string.Compare( RTSPMethodNames.PAUSE , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Pause;
            }

            if ( string.Compare( RTSPMethodNames.PLAY , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Play;
            }

            if ( string.Compare( RTSPMethodNames.RECORD , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Record;
            }

            if ( string.Compare( RTSPMethodNames.REDIRECT , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Redirect;
            }

            if ( string.Compare( RTSPMethodNames.SETUP , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.Setup;
            }

            if ( string.Compare( RTSPMethodNames.SET_PARAMETER , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.SetParameter;
            }

            if ( string.Compare( RTSPMethodNames.TEARDOWN , method , ignoreCase ) == 0 )
            {
                return RTSPMethod.TearDown;
            }

            return RTSPMethod.UnDefined;
        }
    }
}
