using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class CompanyReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Site { get; set; }
        public string? Lieu { get; set; }
    }
}
