using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Entities
{
    public class Interaction : BaseEntity
    {
        public  TypeInteraction Type { get; set; } 
        public string Notes { get; set; } = string.Empty;


        // Clés étrangères
        public int CandidatureId { get; set; }

        // Propriétés de navigation
        public virtual Candidature Candidature { get; set; } = null!;
    }

}
