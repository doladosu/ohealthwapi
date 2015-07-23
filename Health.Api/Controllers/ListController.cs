using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.List;
using Health.Models.Output;
using Health.Setup;
using Health.Setup.Core;

namespace Health.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/List")]
    public class ListController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="queryDispatcher"></param>
        public ListController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Gets all List.States
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("States")]
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<State>))]
        public async Task<IHttpActionResult> GetAllStates(ILoggedInPerson loggedInPerson)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery();
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, StateQueryResult>(baseByIdQuery);
                return Ok(result.States);
            }, memberParameters: new object[] { loggedInPerson });
        }
    }
}
