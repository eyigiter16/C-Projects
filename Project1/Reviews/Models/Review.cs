using System;

namespace Reviews.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int Star { get; set; }
        public string Status { get; set; }
        public string RejectReason { get; set; }
        public Guid OperatedBy { get; set; }
    }
}