using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Site { get; set; }
        public string? Lieu { get; set; }

    }
}
