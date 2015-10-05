using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Health.Data.Core.Command.Appointment;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Appointment;
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
                var baseByIdQuery = new BaseByIdQuery{Id = patientId, UserId = loggedInPerson.Id};
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, AppointmentsQueryResult>(baseByIdQuery);
                return new CustomOkResult<IEnumerable<Appointment>>(result.Appointments, this)
                {
                    XInlineCount = result.TotalRecords.ToString()
                };
            }, memberParameters: new object[] { loggedInPerson, patientId });
        }

        /// <returns></returns>
        [HttpGet]
        [Route("admin")]
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<Appointment>))]
        public async Task<IHttpActionResult> GetAllPatientAppointmentsTaskAdmin(ILoggedInPerson loggedInPerson)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery { UserId = loggedInPerson.Id, Id = 0};
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
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> GetPatientAppointmentTask(ILoggedInPerson loggedInPerson, int appointmentId)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery { Id = appointmentId };
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, AppointmentQueryResult>(baseByIdQuery);
                return Ok(result.Appointment);
            }, memberParameters: new object[] { loggedInPerson, appointmentId });
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
        public async Task<IHttpActionResult> CreatePatientAppointmentTask(ILoggedInPerson loggedInPerson, [FromBody]Appointment appointment)
        {
            return await TryAsync(async () =>
            {
                var command = new CreatePatientAppointmentCommand { Appointment = appointment, UserId = loggedInPerson.Id };
                var result = await CommandDispatcher.Dispatch(command);
                return Ok(result);
            }, memberParameters: new object[] { loggedInPerson, appointment });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(Appointment))]
        public async Task<IHttpActionResult> UpdatePatientAppointmentTask(ILoggedInPerson loggedInPerson, [FromBody]Appointment appointment)
        {
            return await TryAsync(async () =>
            {
                var command = new UpdatePatientAppointmentCommand { Appointment = appointment };
                var result = await CommandDispatcher.Dispatch(command);
                return Ok(result);
            }, memberParameters: new object[] { loggedInPerson, appointment });
        }
    }
}
