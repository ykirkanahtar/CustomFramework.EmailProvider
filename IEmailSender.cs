using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomFramework.EmailProvider
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, IList<string> toList, string subject, string message, bool isBodyHtml);
        Task SendEmailAsync(string from, string to, string subject, string message, bool isBodyHtml);
    }
}