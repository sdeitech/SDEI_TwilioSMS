﻿using SD_SMS.Models;
using Twilio.Rest.Api.V2010.Account;

namespace SD_SMS.Services
{
    public interface ISMSService
    {

        /// <summary>
        /// This method is being used to send SMS using Twilio to given Phone number
        /// </summary>
        /// <param name="request">Request object having PhoneNo and Message to send.</param>
        /// <returns></returns>
        Task<ResponseModel> SendSMSAsync(SMSRequest request);

        /// <summary>
        /// This method is being used to retrieve the status of the message by SID generated by Twilio.
        /// </summary>
        /// <param name="sid">This is the SID of the message that gets generated whenever we request to send the message.</param>
        /// <returns></returns>
        Task<ResponseModel> RetrieveStatusAsyc(string sid);
    }
}
