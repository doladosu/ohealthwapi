﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.QueryService.Repository
{
    public interface IListQueryRepository : IRepository
    {
        Task<IEnumerable<State>> GetAllStates();
    }
}