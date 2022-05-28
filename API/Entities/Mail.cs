using System;

namespace Entities
{
    public class Mail : BaseEntity
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public bool IsDeleted { get; set; }
    }
}