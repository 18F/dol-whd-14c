﻿using System;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business
{
    public interface IStatusService : IDisposable
    {
        Status GetStatus(int id);
    }
}
