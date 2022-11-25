namespace SD_SMS.Helpers
{
    public class Constants
    {
        public const string SMSSuccessMessage = "SMS sent successfully!";
        public const string SMSFailureMessage= "Failed to send the SMS!";
        public const string SMSExceptionMessage = "Exception occurred while processing the request!";
        public const string GeneralSuccessMessage = "Successful!";
        public const string RetrieveFailureMessage = "Failed to retrieve status of the SMS!";
        public const string InvalidSidPassed = "Invalid SID. Please check and pass again!";

        public const string SMSMFASuccessMessage = "MFA sms sent successfully!";
        public const string SMSMFAFailureMessage = "Failed to sent MFA SMS!";
        public const string SMSMFAVerifyFailureMessage = "Failed to verify MFA status!";
    }
}
