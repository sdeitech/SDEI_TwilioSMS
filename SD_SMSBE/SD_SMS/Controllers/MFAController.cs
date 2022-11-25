using Microsoft.AspNetCore.Mvc;
using SD_SMS.Models;
using SD_SMS.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SD_SMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MFAController : ControllerBase
    {
        private readonly IMFAService _mfaService;
        public MFAController(IMFAService mfaService)
        {
            _mfaService = mfaService;
        }

        /// <summary>
        /// This endpoint is being used to send MFA code to given phone number and returns the status after that.
        /// </summary>
        /// <param name="request">Request object having PhoneNo and custom Message to send.</param>
        /// <remarks>This endpoint uses twilio verify library to send a random MFA code along with custom message to given Phone number</remarks>
        /// <returns>Returns the status based on input model.</returns>
        /// <response code="200">MFA sent published successully. Response will have Sid and Status to track.</response>
        /// <response code="400">The provided input is not valid.</response>
        /// <response code="500">Exception occured while processing the request.</response>
        /// <response code="502">Failure from Twilio while attempting to send MFA code.</response>
        [Route("Send")]
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SendMFACodeAsync([FromBody] MFARequest request)
        {
            // Validating the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid Entry!!" });
            }
            var smsSendResponse = await _mfaService.CreateMFAAsync(request);
            return StatusCode(smsSendResponse.Status, smsSendResponse);
        }

        /// <summary>
        /// This endpoint is being used to verify the MFA code delivered to the given phone number and returns the verification status after that.
        /// </summary>
        /// <param name="request">Request object having PhoneNo and Verification code to validate.</param>
        /// <remarks>This endpoint uses twilio verify library to send a random MFA code along with custom message to given Phone number</remarks>
        /// <returns>Returns the status based on input model.</returns>
        /// <response code="200">MFA status found successfully. Response will have Sid and MFA status.</response>
        /// <response code="400">The provided input is not valid.</response>
        /// <response code="500">Exception occured while processing the request.</response>
        /// <response code="502">Failure from Twilio while attempting to verify MFA status.</response>
        [Route("Verify")]
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> VerifyAsync([FromBody] MFARequest request)
        {
            // Validating the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid Entry!!" });
            }
            var smsSendResponse = await _mfaService.VerifyMFACodeAsync(request);
            return StatusCode(smsSendResponse.Status, smsSendResponse);
        }

    }
}
