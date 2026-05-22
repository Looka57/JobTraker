using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class InteractionReadDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public TypeInteraction Type { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int CandidatureId { get; set; }
    }
}
