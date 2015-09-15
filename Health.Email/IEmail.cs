using System.Collections.Generic;
using System.Threading.Tasks;

namespace Health.Email
{
    public interface IEmail
    {
        Task SendEmail(string subject, string fromEmail, string fromName, List<string> toEmailsList, string plainBody, string htmlBody);
    }
}