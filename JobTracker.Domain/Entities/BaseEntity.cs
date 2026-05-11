using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } // Identifiant unique pour toutes les tables
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }

}
