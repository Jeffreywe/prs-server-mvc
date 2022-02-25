using System;
using System.Collections.Generic;

#nullable disable

namespace prs_server_mvc.Models
{
    public partial class Request
    {
        public Request()
        {
            RequestLines = new HashSet<RequestLine>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<RequestLine> RequestLines { get; set; }
    }
}
