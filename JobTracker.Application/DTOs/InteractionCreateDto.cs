using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class InteractionCreateDto
    {
        public TypeInteraction Type { get; set; }
        public string Notes { get; set; } = string.Empty;

        public int CandidatureId { get; set; }
    }
}
