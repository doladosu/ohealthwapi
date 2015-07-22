using Health.Data.Context;

namespace Health.Data.CommandService.Repository.Impl
{
    public class BaseCommandRepository
    {
        public readonly HealthDbContext Db;

        public BaseCommandRepository()
        {
            Db = new HealthDbContext();
        }
    }
}
