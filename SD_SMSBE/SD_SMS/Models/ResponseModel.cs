namespace SD_SMS.Models
{
    public class ResponseModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>

        public ResponseModel()
        {
            Errors = new List<Error>();
        }
        public int Status { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
        public List<Error> Errors { get; set; }
    }

    /// <summary>
    /// Standard Error Response
    /// </summary>
    public class Error
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string More_Info { get; set; }
    }
}
