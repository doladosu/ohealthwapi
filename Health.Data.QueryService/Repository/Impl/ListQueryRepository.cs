using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Health.Models.Output;

namespace Health.Data.QueryService.Repository.Impl
{
    public class ListQueryRepository : BaseQueryRepository, IListQueryRepository
    {
        public async Task<IEnumerable<State>> GetAllStates()
        {
            var allStates = await Db.States.ToListAsync();
            return Mapper.Map<IEnumerable<State>>(allStates);
        }
    }
}