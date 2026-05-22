using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Enums
{
    public enum TypeInteraction
    {
        ContactInitial = 0,
        Relancement = 1,
        AppelRh = 2,
        Entretiens = 3,
        EntretienTechnique = 4,
        EntretienFinal = 5,
        OffreRecu = 6,
        Refus = 7
    }
}
