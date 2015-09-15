using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Health.Data.Core.Command;
using Health.Data.Core.Command.Patient;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Appointment;
using Health.Data.Core.QueryResult.Patient;
using Health.Models.Output;
using Health.Setup;
using Health.Setup.Core;

namespace Health.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Appointments")]

    public class AppointmentController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="queryDispatcher"></param>
        public AppointmentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Return all appointments for patient
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{patientId:int}")]
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<Appointment>))]
        public async Task<IHttpActionResult> GetAllPatientAppointmentsTask(ILoggedInPerson loggedInPerson, int patientId)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery{Id = patientId};
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, AppointmentsQueryResult>(baseByIdQuery);
                return new CustomOkResult<IEnumerable<Appointment>>(result.Appointments, this)
                {
                    XInlineCount = result.TotalRecords.ToString()
                };
            }, memberParameters: new object[] { loggedInPerson });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> GetPatientAppointmentTask(ILoggedInPerson loggedInPerson, int id)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery { Id = id };
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, AppointmentQueryResult>(baseByIdQuery);
                return Ok(result.Appointment);
            }, memberParameters: new object[] { loggedInPerson, id });
        }

        /// <summary>
        /// Create new patient profile
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(CommandResult))]
        public async Task<IHttpActionResult> CreatePatientAppointmentTask(ILoggedInPerson loggedInPerson, Appointment appointment)
        {
            return await TryAsync(async () =>
            {
                var command = new CreatePatientProfileCommand { PatientProfile = null, UserId = loggedInPerson.Id };
                var result = await CommandDispatcher.Dispatch(command);
                return Ok(result);
            }, memberParameters: new object[] { loggedInPerson });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> UpdatePatientAppointmentTask(ILoggedInPerson loggedInPerson, int id)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery { Id = id };
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, AppointmentQueryResult>(baseByIdQuery);
                return Ok(result.Appointment);
            }, memberParameters: new object[] { loggedInPerson, id });
        }
    }
}
