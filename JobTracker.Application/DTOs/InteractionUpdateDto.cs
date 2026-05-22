using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class InteractionUpdateDto
    {
        public TypeInteraction Type { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
