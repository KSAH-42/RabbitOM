using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the RTSP status code <see href="https://www.iana.org/assignments/rtsp-parameters/rtsp-parameters.xhtml"/>
    /// </summary>
    public enum RTSPStatusCode
    {
        /// <summary>
        /// Invalid code value
        /// </summary>
        UnDefined                         = 0,

        /// <summary>
        /// Represent a status code
        /// </summary>
        Continue                          = 100,

        /// <summary>
        /// Represent a status code
        /// </summary>
        OK                                = 200,

        /// <summary>
        /// Represent a status code
        /// </summary>
        Created                           = 201,

        /// <summary>
        /// Represent a status code
        /// </summary>
        LowOnStorageSpace                 = 250,

        /// <summary>
        /// Represent a status code
        /// </summary>
        MultipleChoices                   = 300,

        /// <summary>
        /// Represent a status code
        /// </summary>
        MovedPermanently                  = 301,

        /// <summary>
        /// Represent a status code
        /// </summary>
        MovedTemporarily                  = 302,

        /// <summary>
        /// Represent a status code
        /// </summary>
        SeeOther                          = 303,

        /// <summary>
        /// Represent a status code
        /// </summary>
        NotModified                       = 304,

        /// <summary>
        /// Represent a status code
        /// </summary>
        UseProxy                          = 305,

        /// <summary>
        /// Represent a status code
        /// </summary>
        BadRequest                        = 400,

        /// <summary>
        /// Represent a status code
        /// </summary>
        UnAuthorized                      = 401,

        /// <summary>
        /// Represent a status code
        /// </summary>
        PaymentRequired                   = 402,

        /// <summary>
        /// Represent a status code
        /// </summary>
        Forbidden                         = 403,

        /// <summary>
        /// Represent a status code
        /// </summary>
        NotFound                          = 404,

        /// <summary>
        /// Represent a status code
        /// </summary>
        MethodNotAllowed                  = 405,

        /// <summary>
        /// Represent a status code
        /// </summary>
        NotAcceptable                     = 406,

        /// <summary>
        /// Represent a status code
        /// </summary>
        ProxyAuthenticationRequired       = 407,

        /// <summary>
        /// Represent a status code
        /// </summary>
        RequestTimeout                    = 408,

        /// <summary>
        /// Represent a status code
        /// </summary>
        Gone                              = 410,

        /// <summary>
        /// Represent a status code
        /// </summary>
        LengthRequired                    = 411,

        /// <summary>
        /// Represent a status code
        /// </summary>
        PreconditionFailed                = 412,

        /// <summary>
        /// Represent a status code
        /// </summary>
        RequestEntityTooLarge             = 413,

        /// <summary>
        /// Represent a status code
        /// </summary>
        RequestUriTooLarge                = 414,

        /// <summary>
        /// Represent a status code
        /// </summary>
        UnSupportedMediaType              = 415,

        /// <summary>
        /// Represent a status code
        /// </summary>
        ParameterNotUnderstood            = 451,

        /// <summary>
        /// Represent a status code
        /// </summary>
        ConferenceNotFound                = 452,

        /// <summary>
        /// Represent a status code
        /// </summary>
        NotEnoughBandwidth                = 453,

        /// <summary>
        /// Represent a status code
        /// </summary>
        SessionNotFound                   = 454,

        /// <summary>
        /// Represent a status code
        /// </summary>
        MethodNotValidInThisState         = 455,

        /// <summary>
        /// Represent a status code
        /// </summary>
        HeaderFieldNotValidForResource    = 456,

        /// <summary>
        /// Represent a status code
        /// </summary>
        InvalidRange                      = 457,

        /// <summary>
        /// Represent a status code
        /// </summary>
        ParameterIsReadOnly               = 458,

        /// <summary>
        /// Represent a status code
        /// </summary>
        AggregateOperationNotAllowed      = 459,

        /// <summary>
        /// Represent a status code
        /// </summary>
        OnlyAggregateOperationAllowed     = 460,

        /// <summary>
        /// Represent a status code
        /// </summary>
        UnSupportedTransport              = 461,

        /// <summary>
        /// Represent a status code
        /// </summary>
        DestinationUnReachable            = 462,

        /// <summary>
        /// Represent a status code
        /// </summary>
        KeyManagementFailure              = 463,

        /// <summary>
        /// Represent a status code
        /// </summary>
        InternalServerError               = 500,

        /// <summary>
        /// Represent a status code
        /// </summary>
        NotImplemented                    = 501,

        /// <summary>
        /// Represent a status code
        /// </summary>
        BadGateway                        = 502,

        /// <summary>
        /// Represent a status code
        /// </summary>
        ServiceUnavailable                = 503,

        /// <summary>
        /// Represent a status code
        /// </summary>
        GatewayTimeout                    = 504,

        /// <summary>
        /// Represent a status code
        /// </summary>
        VersionNotSupported               = 505,

        /// <summary>
        /// Represent a status code
        /// </summary>
        OptionNotSupported                = 551,
    }
}
