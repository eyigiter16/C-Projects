namespace Reviews.Models
{
    public class Review
    {
        public int id { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public string star { get; set; }
        public string status { get; set; }
        public string rejectReason { get; set; }
        public int operatedBy { get; set; }
    }
}