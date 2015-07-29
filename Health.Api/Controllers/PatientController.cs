using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Health.Data.Core.Command;
using Health.Data.Core.Command.Patient;
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
    [Authorize]
    [RoutePrefix("api/Patients")]
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
        [ResponseType(typeof(IEnumerable<Patient>))]
        public async Task<IHttpActionResult> GetAllPatientProfilesTask(ILoggedInPerson loggedInPerson)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery{UserId = loggedInPerson.Id};
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, PatientProfilesQueryResult>(baseByIdQuery);
                return new CustomOkResult<IEnumerable<Patient>>(result.Patients, this)
                {
                    XInlineCount = result.TotalRecords.ToString()
                };
            }, memberParameters: new object[] { loggedInPerson });
        }

        /// <summary>
        /// Gets PatientProfile by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(PatientProfile))]
        public async Task<IHttpActionResult> GetPatientProfileTask(ILoggedInPerson loggedInPerson, int id)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery {Id = id};
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, PatientProfileQueryResult>(baseByIdQuery);
                return Ok(result.PatientProfile);
            }, memberParameters: new object[] { loggedInPerson, id });
        }

        /// <summary>
        /// Create new patient profile
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="patientProfile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(CommandResult))]
        public async Task<IHttpActionResult> CreatePatientProfileTask(ILoggedInPerson loggedInPerson, PatientProfile patientProfile)
        {
            return await TryAsync(async () =>
            {
                var command = new CreatePatientProfileCommand{PatientProfile = patientProfile, UserId = loggedInPerson.Id};
                var result = await CommandDispatcher.Dispatch(command);
                return Ok(result);
            }, memberParameters: new object[] { loggedInPerson, patientProfile });
        }
    }
}
