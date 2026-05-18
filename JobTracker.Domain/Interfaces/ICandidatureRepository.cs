using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Interfaces
{
    public interface ICandidatureRepository : IRepository<Candidature>
    {
        //charger les entreprises
        Task<IEnumerable<Candidature>> GetCandidaturesWithCompanyAsync();

        //corriger le GET par ID
        Task<Candidature?> GetByIdWithCompanyAsync(int id);
    }
}
