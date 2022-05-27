using System;

namespace Entities
{
    public class Mail : BaseEntity
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}