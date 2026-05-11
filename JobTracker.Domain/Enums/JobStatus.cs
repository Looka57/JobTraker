using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Enums
{
    public enum JobStatus
    {
        Brouillon = 0,
        Envoyée = 1,
        Suivi = 2,
        Entretien = 3,
        Accepté = 4,
        Refusé = 5
    }

}
