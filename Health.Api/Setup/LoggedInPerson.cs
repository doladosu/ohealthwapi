using System.Collections.Generic;

namespace Health.Setup
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggedInPerson : ILoggedInPerson
    {
        /// <summary>
        /// Logged in person information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <param name="actorId"></param>
        /// <param name="securityUserId"></param>
        public LoggedInPerson(string id, string userName, List<string> roles, int actorId, int securityUserId, bool? isEnabled)
        {
            Id = id;
            UserName = userName;
            Roles = roles;
            ActorId = actorId;
            SecurityUserId = securityUserId;
            IsEnabled = isEnabled;
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public List<string> Roles { get; private set; }
        public int ActorId { get; private set; }
        public int SecurityUserId { get; private set; }
        public bool? IsEnabled { get; set; }
    }
}