using System;

namespace Entities
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}