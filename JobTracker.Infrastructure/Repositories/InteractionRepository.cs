using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Infrastructure.Repositories
{
    public class InteractionRepository : Repository<Interaction>, IInteractionRepository
    {
        public InteractionRepository(JobTrackDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Interaction>> GetInteractionsByCandidatureIdAsync(int candidatureId)
        {
            return await _context.Interactions
                .Where(i => i.CandidatureId == candidatureId)
        .ToListAsync();
        }
    }
}
