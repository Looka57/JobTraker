using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Interfaces
{
    public interface IInteractionRepository : IRepository<Interaction>
    {
        // Permet de récupérer toutes les interactions d'une candidature précise
        Task<IEnumerable<Interaction>> GetInteractionsByCandidatureIdAsync(int candidatureId);
        
    }
}
