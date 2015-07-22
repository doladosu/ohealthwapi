using Health.Data.Context;

namespace Health.Data.QueryService.Repository.Impl
{
    public class BaseQueryRepository
    {
        public readonly HealthDbContext Db;

        public BaseQueryRepository()
        {
            Db = new HealthDbContext();
        }
    }
}