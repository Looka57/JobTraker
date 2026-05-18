using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Entities
{
    public class Candidature: BaseEntity
    {
        public string? Poste { get; set; } 
        public string? TypeContrat { get; set; }
        public decimal? Salaire { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Envoyée;
        public DateTime DateCandidature { get; set; } = DateTime.UtcNow;
        public int NiveauMotivation { get; set; }
        public string? UrlOffre { get; set; }

        // Clés étrangères
        public int CompanyId { get; set; }
        //public int UserId { get; set; }

        // Propriétés de navigation
        public virtual Company Company { get; set; } = null!;
        //public virtual User User { get; set; } = null!;
        //public virtual ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();




        //Virtual : J'ai ajouté le mot-clé virtual sur les collections. Cela permet à Entity Framework de faire du "Lazy Loading" (charger les données liées seulement quand tu en as besoin).


    }
}
