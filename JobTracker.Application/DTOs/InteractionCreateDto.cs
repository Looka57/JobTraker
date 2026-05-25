using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobTracker.Application.DTOs
{
    public class InteractionCreateDto
    {
        [Required(ErrorMessage = "Le type d'interaction est obligatoire.")]
        public TypeInteraction Type { get; set; }

        [StringLength(500, ErrorMessage = "Les notes ne peuvent pas dépasser 500 caractères.")]
        public string Notes { get; set; } = string.Empty;

        [Required(ErrorMessage = "La candidature associée est obligatoire.")]
        public int CandidatureId { get; set; }
    }
}
