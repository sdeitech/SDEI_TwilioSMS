using Microsoft.AspNetCore.Mvc;
using SD_SMS.Models;
using SD_SMS.Services;
using Twilio.Http;

namespace SD_SMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISMSService _smsService;
        public SMSController(ISMSService smsService)
        {
            _smsService = smsService;
        }

        /// <summary>
        /// This endpoint is being used to send SMS to given phone number and returns the status after that.
        /// </summary>
        /// <param name="request">Request object having PhoneNo and Message to send.</param>
        /// <remarks>This endpoint uses twilio sms library to send given sms text to given Phone number</remarks>
        /// <returns>Returns the status based on input model.</returns>
        /// <response code="200">SMS request published successully. Response will have Sid and Status to track.</response>
        /// <response code="400">The provided input is not valid.</response>
        /// <response code="500">Exception occured while processing the request.</response>
        /// <response code="502">Failure from Twilio while attempting to send the SMS.</response>
        [Route("Send")]
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SendSMSAsync([FromBody] SMSRequest request)
        {
            // Validating the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid Entry!!" });
            }
            var smsSendResponse=await _smsService.SendSMSAsync(request);
            return StatusCode(smsSendResponse.Status, smsSendResponse);
        }


        /// <summary>
        /// This method is being used to retrieve the status of the message by SID generated by Twilio.
        /// </summary>
        /// <param name="sid">This is the SID of the message that gets generated whenever we request to send the message.</param>
        /// <remarks>This endoint calls Twilio fetch resource and retrieve status, error and error messages from twilio</remarks>
        /// <returns>SMS Status having Id, Current status, Error(if any) and Error message(if any)</returns>
        /// <response code="200">SMS status with Id, Current status, Error and Error properties.</response>
        /// <response code="400">The provided Sid is not valid.</response>
        /// <response code="500">Exception occured while processing the request.</response>
        /// <response code="502">Failure from Twilio while attempting to get the status of SMS.</response>
        [Route("{sid}/Status")]
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> GetStatusAsync(string sid)
        {
            var statusResponse = await _smsService.RetrieveStatusAsyc(sid);
            return StatusCode(statusResponse.Status, statusResponse);
        }
    }
}
