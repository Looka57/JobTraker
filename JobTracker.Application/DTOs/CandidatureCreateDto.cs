using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class CandidatureCreateDto
    {
        [Required(ErrorMessage = "Le titre du poste est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le titre du poste ne peut pas dépasser 100 caractères.")]
        public string Poste { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le type de contrat est obligatoire.")]
        public string TypeContrat { get; set; } = string.Empty;

        [Range(0, 9999999, ErrorMessage = "Le salaire ne peut pas être négatif.")]
        public decimal? Salaire { get; set; }

        public JobStatus Status { get; set; } = JobStatus.Envoyée;

        [Range(1, 5, ErrorMessage = "Le niveau de motivation doit être compris entre 1 et 5.")]
        public int NiveauMotivation { get; set; }

        [Url(ErrorMessage = "L'URL de l'offre n'est pas valide.")]
        public string? UrlOffre { get; set; }

        [Required(ErrorMessage = "L'entreprise associée est obligatoire.")]
        public string? CompagnyName { get; set; }
    }
}
