namespace DOL.WHD.Section14c.Domain.ViewModels.Summary
{
    public class ResponseSummary
    {
        public int Id { get; set; }

        /// <summary>
        /// The value of the response to display on the form
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// An optional shorter display to be used on the admin summary table
        /// </summary>
        public string ShortDisplay { get; set; }
    }
}
