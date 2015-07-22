using System.Collections.Generic;
using Health.Models.Output;

namespace Health.Data.Core.QueryResult.List
{
    public class StateQueryResult : BaseQueryResult
    {
        public IEnumerable<State> States { get; set; }
    }
}
