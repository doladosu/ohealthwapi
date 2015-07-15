using Health.Setup.Core;

namespace Health.Data.Core.Query
{
    public class BaseByIdQuery : IQuery
    {
        public int Id { get; set; }
    }
}