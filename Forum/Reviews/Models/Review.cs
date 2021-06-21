using System;

namespace Reviews.Models
{
    public class Review : Document
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int Star { get; set; }
        public string Status { get; set; }
        public string RejectReason { get; set; }
        public Guid OperatedBy { get; set; }
    }
}