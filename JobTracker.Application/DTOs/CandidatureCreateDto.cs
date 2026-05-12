using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class CandidatureCreateDto
    {
        public string Poste { get; set; } = string.Empty;

        public string TypeContrat { get; set; } = string.Empty;

        public decimal? Salaire { get; set; }

        public JobStatus Status { get; set; } = JobStatus.Envoyée;

        public int NiveauMotivation { get; set; }

        public string? UrlOffre { get; set; }

        public int CompanyId { get; set; }
    }
}
