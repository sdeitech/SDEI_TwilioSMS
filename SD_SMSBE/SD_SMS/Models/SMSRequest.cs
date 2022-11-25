using System.ComponentModel.DataAnnotations;

namespace SD_SMS.Models
{
    public class SMSRequest
    {
        /// <summary>
        /// This is the phone number where we need to deliver MFA code
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0|\+[0-9]{1,5})?([0-9]{11})$", ErrorMessage = "Not a valid Phone number")]
        [StringLength(15, ErrorMessage = "Phone Number length Should be less than 15")]
        public string PhoneNo
        {
            // Removing the Special charaters and making the model state valid 
            get
            {
                return _PhoneNo;
            }
            set
            {
                // Since twilio did not accept any number with "+" so we are here appending "+" in front of the number(if no avaialable).
                // Changing the format of the phone number(if number is (789) 123 4567 or 123-456-7890 or 123 456 7890).
                _PhoneNo = value.Contains("+") ? value : "+" + value;
                _PhoneNo = _PhoneNo.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            }

        }

        /// <summary>
        /// The message which we want to deliver through SMS
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message { get; set; }

        private string _PhoneNo { get; set; }
    }
}
