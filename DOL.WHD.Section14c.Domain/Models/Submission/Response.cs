namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Response : BaseEntity
    {
        public int Id { get; set; }
        
        public string QuestionKey { get; set; }

        public string Display { get; set; }

        public string SubDisplay { get; set; }

        public string OtherValueKey { get; set; }

        public bool IsActive { get; set; }
    }
}
