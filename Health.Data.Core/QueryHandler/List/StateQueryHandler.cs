using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.List;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.List
{
    public class StateQueryHandler : IQueryHandler<BaseByIdQuery, StateQueryResult>
    {
        private readonly IListQueryRepository _listQueryRepository;

        public StateQueryHandler(IListQueryRepository listQueryRepository)
        {
            _listQueryRepository = listQueryRepository;
        }

        public async Task<StateQueryResult> Retrieve(BaseByIdQuery query)
        {
            var states = await _listQueryRepository.GetAllStates();

            return new StateQueryResult
            {
                States = states
            };
        }
    }
}