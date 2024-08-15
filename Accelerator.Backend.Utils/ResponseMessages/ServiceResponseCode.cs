namespace Accelerator.Backend.Utils.ResponseMessages;

/// <summary>
/// Codes to response de logic services
/// </summary>
public enum ServiceResponseCode
{
    /// <summary>
    /// The success
    /// </summary>
    Success = 200,
    /// <summary>
    /// The internal error
    /// </summary>
    InternalError = 500,
    /// <summary>
    /// The service external error
    /// </summary>
    ServiceExternalError = 501,

    /// <summary>
    /// The bad request
    /// </summary>
    BadRequest = 102,

    /// <summary>
    /// The invalid credentials
    /// </summary>
    InvalidCredentials = 103,

    /// <summary>
    /// The affiliate not found
    /// </summary>
    AffiliateNotFound = 104,

    /// <summary>
    /// The affiliate not in pc
    /// </summary>
    AffiliateNotInPC = 105,

    /// <summary>
    /// The error cancel appointment.
    /// </summary>
    ErrorCancelAppointment = 106,

    /// <summary>
    /// The error authentication.
    /// </summary>
    ErrorAuthentication = 107,

    /// <summary>
    /// The record not found
    /// </summary>
    RecordNotFound = 108,

    /// Code External

    /// <summary>
    /// The invalid password.
    /// </summary>
    InvalidPassword = 1001,

    /// <summary>
    /// The new password.
    /// </summary>
    NewPassword = 1002,

    /// <summary>
    /// The inactive user.
    /// </summary>
    InactiveUser = 1003,

    /// <summary>
    /// The activation pending.
    /// </summary>
    ActivationPending = 1004,

    /// <summary>
    /// The locked user.
    /// </summary>
    LockedUser = 1005,

    /// <summary>
    /// The user not found
    /// </summary>
    UserNotFound = 105
}
