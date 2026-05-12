using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class UpdateCandidatureDto
    {
        public string Poste { get; set; } = string.Empty;

        public string TypeContrat { get; set; } = string.Empty;

        public decimal? Salaire { get; set; }

        public JobStatus Status { get; set; }

        public int NiveauMotivation { get; set; }

        public string? UrlOffre { get; set; }
    }
}
