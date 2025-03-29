using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
