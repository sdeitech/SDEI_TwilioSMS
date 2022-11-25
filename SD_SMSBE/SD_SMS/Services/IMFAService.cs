using SD_SMS.Models;

namespace SD_SMS.Services
{
    public interface IMFAService
    {
        /// <summary>
        /// This method is being used to send MFA verification code using Twilio to given Phone number
        /// </summary>
        /// <param name="request">Request object having PhoneNo and Custom Message to send.</param>
        /// <returns></returns>
        Task<ResponseModel> CreateMFAAsync(MFARequest request);

        /// <summary>
        /// This method is being used to validate the MFA code that was sent to the phone number
        /// </summary>
        /// <param name="request">Request object having PhoneNo and verification code to send.</param>
        /// <returns></returns>
        Task<ResponseModel> VerifyMFACodeAsync(MFARequest request);
    }
}
