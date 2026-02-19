using System;
using System.Collections.Generic;
using System.Text;

namespace DreamJourney.Data.Models.Enums
{
    public enum ApplicationStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,

        PaymentAuthorized = 3,
        Completed = 4
    }
}
