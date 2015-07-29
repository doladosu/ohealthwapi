using System.Collections.Generic;

namespace Health.Setup
{
    /// <summary>
    /// Properties of loggedIn user
    /// </summary>
    public interface ILoggedInPerson
    {
        /// <summary>
        /// 
        /// </summary>
        string Id { get; }
        /// <summary>
        /// 
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 
        /// </summary>
        List<string> Roles { get; }
    }
}