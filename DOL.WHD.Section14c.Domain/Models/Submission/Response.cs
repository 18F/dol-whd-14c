namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Response : BaseEntity
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Key used on front end to match response to question
        /// </summary>
        public string QuestionKey { get; set; }

        /// <summary>
        /// The value of the response to display on the form
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// An optional line of text to display below the display
        /// </summary>
        public string SubDisplay { get; set; }

        /// <summary>
        /// An optional shorter display to be used on the admin summary table
        /// </summary>
        public string ShortDisplay { get; set; }

        /// <summary>
        /// If this response is an "Other" response, the property name to use for collecting the other response
        /// </summary>
        public string OtherValueKey { get; set; }

        /// <summary>
        /// Whether or not the response should be shown on the form
        /// </summary>
        public bool IsActive { get; set; }
    }
}
