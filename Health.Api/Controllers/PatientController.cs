using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Models.Output;
using Health.Setup;
using Health.Setup.Core;

namespace Health.Controllers
{
    /// <summary>
    /// Patient endpoints
    /// </summary>
    //[Authorize]
    [RoutePrefix("api/Patient")]
    public class PatientController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="queryDispatcher"></param>
        public PatientController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Gets all PatientProfiles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<PatientProfile>))]
        public async Task<IHttpActionResult> GetAllPatientProfilesTask(ILoggedInPerson loggedInPerson)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery();
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, PatientProfileQueryResult>(baseByIdQuery);
                return new CustomOkResult<IEnumerable<PatientProfile>>(result.PatientProfiles, this)
                {
                    XInlineCount = result.TotalRecords.ToString()
                };
            }, memberParameters: new object[] { loggedInPerson });
        }
    }
}
