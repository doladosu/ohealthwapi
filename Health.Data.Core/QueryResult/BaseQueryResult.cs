using Health.Setup.Core;

namespace Health.Data.Core.QueryResult
{
    public class BaseQueryResult : IQueryResult
    {
        public int TotalRecords { get; set; }
    }
}