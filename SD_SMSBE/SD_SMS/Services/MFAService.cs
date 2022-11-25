using Microsoft.Extensions.Options;
using SD_SMS.Helpers;
using SD_SMS.Helpers.Enums;
using SD_SMS.Models;
using System.Net;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;

namespace SD_SMS.Services
{
    public class MFAService:IMFAService
    {
        private readonly IOptions<TwilioConfig> _twilioConfig;
        private readonly ILogger _logger;
        public MFAService(ILogger<MFAService> logger, IOptions<TwilioConfig> twilioConfig)
        {
            _logger = logger;
            _twilioConfig = twilioConfig;
            // Initializing Twilio client by Twilio Id and Token.
            TwilioClient.Init(_twilioConfig.Value.TwilioId, _twilioConfig.Value.TwilioToken);
        }

        /// <summary>
        /// This method is being used to send MFA verification code using Twilio to given Phone number
        /// </summary>
        /// <param name="request">Request object having PhoneNo and Custom Message to send.</param>
        /// <returns></returns>
        public async Task<ResponseModel> CreateMFAAsync(MFARequest request)
        {
            ResponseModel response = new();
            try
            {
                
                var verification = await VerificationResource.CreateAsync(
                    to: request.PhoneNo,
                    channel: MFAChannels.sms.ToString(),
                    pathServiceSid: _twilioConfig.Value.SMSMFAServiceId,
                    customMessage: request.Message
                );

                if (verification?.Status != null)
                {
                    response.Status = (int)HttpStatusCode.OK;
                    response.Message = Constants.SMSMFASuccessMessage;
                    response.Response = new { verification.Sid, MessageStatus = verification?.Status };
                }
                else
                {
                    response.Status = (int)HttpStatusCode.BadGateway;
                    response.Message = Constants.SMSMFAFailureMessage;
                    response.Response = new { verification?.Sid, MessageStatus = verification?.Status };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Constants.SMSExceptionMessage}: Exception: {ex.Message}. Stack Trace: {ex.StackTrace}");
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = Constants.SMSExceptionMessage;
                response.Errors.Add(
                    new Error
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message,
                        Description = ex.InnerException?.Message ?? ex.Message,
                        More_Info = ex?.StackTrace
                    });
            }
            return response;
        }

        /// <summary>
        /// This method is being used to validate the MFA code that was sent to the phone number
        /// </summary>
        /// <param name="request">Request object having PhoneNo and verification code to send.</param>
        /// <returns></returns>
        public async Task<ResponseModel> VerifyMFACodeAsync(MFARequest request)
        {
            ResponseModel response = new();
            try
            {

                var verificationCheck = await VerificationCheckResource.CreateAsync(
                    to: request.PhoneNo,
                    code: request.VerificationCode,
                    pathServiceSid: _twilioConfig.Value.SMSMFAServiceId
                    );
                if (verificationCheck?.Status != null)
                {
                    response.Status = (int)HttpStatusCode.OK;
                    response.Message = Constants.GeneralSuccessMessage;
                    response.Response = new { verificationCheck.Sid, MessageStatus = verificationCheck?.Status };
                }
                else
                {
                    response.Status = (int)HttpStatusCode.BadGateway;
                    response.Message = Constants.SMSMFAVerifyFailureMessage;
                    response.Response = new { verificationCheck?.Sid, MessageStatus = verificationCheck?.Status };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Constants.SMSExceptionMessage}: Exception: {ex.Message}. Stack Trace: {ex.StackTrace}");
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = Constants.SMSExceptionMessage;
                response.Errors.Add(
                    new Error
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message,
                        Description = ex.InnerException?.Message ?? ex.Message,
                        More_Info = ex?.StackTrace
                    });
            }
            return response;
        }
    }
}
