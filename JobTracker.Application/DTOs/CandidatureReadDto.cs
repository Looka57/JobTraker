using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class CandidatureReadDto
    {
        public int Id { get; set; }

        public string Poste { get; set; } = string.Empty;

        public string TypeContrat { get; set; } = string.Empty;

        public decimal? Salaire { get; set; }

        public JobStatus Status { get; set; }

        public string StatusLibelle => Status.ToString();

        public DateTime DateCandidature { get; set; }

        public int NiveauMotivation { get; set; }   

        public string? UrlOffre { get; set; }


        // Pour l'affichage dans le Front POUR LA COMPAGNY
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
