using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Umbrella.Infrastructure.Email.Tests")]

namespace Umbrella.Infrastructure.Email.Core
{
    /// <summary>
    /// Abstraction for Sending email capabilities
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the message
        /// </summary>
        /// <param name="emailMetadata"></param>
        /// <returns>in as of success, the UUID of the send email. Otherwise the occurred error</returns>
        Task<EmailSendResult> SendAsync(EmailMetadata emailMetadata);
    }

    /// <summary>
    /// 
    /// </summary>
    internal class EmailService : IEmailService
    {
        #region Fields
        readonly IEmailSender _Sender;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmailService(IEmailSender sender)
        {
            this._Sender = sender ?? throw new ArgumentNullException(nameof(sender));

        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<EmailSendResult> SendAsync(EmailMetadata emailMetadata)
        {
            try
            {
                var result = await this._Sender.SendAsync(emailMetadata);
                return new EmailSendResult
                {
                    RequestId = result
                };
            }
            catch (Exception ex)
            {
                return new EmailSendResult
                {
                    RequestId = null,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}