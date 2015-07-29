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
        public LoggedInPerson(string id, string userName, List<string> roles)
        {
            Id = id;
            UserName = userName;
            Roles = roles;
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public List<string> Roles { get; private set; }
    }
}