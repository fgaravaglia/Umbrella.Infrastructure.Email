using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.Email.Core
{
    /// <summary>
    /// Abstraction of the mediator for delivering the message
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the message
        /// </summary>
        /// <param name="emailMetadata"></param>
        /// <returns>in as of success, the UUID of the send email. Otherwise the occurred error</returns>
        Task<string> SendAsync(EmailMetadata emailMetadata);
    }
}