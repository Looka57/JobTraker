using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class CandidatureUpdateDto
    {
        public string Poste { get; set; } = string.Empty;

        public string TypeContrat { get; set; } = string.Empty;

        public decimal? Salaire { get; set; }

        public JobStatus Status { get; set; }

        public int NiveauMotivation { get; set; }

        public string? UrlOffre { get; set; }


        // On ajoute l'ID pour pouvoir lier la candidature à une autre entreprise
        public int EntrepriseId { get; set; }
    }
}