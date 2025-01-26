using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.Email.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailSendResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess => !string.IsNullOrEmpty(this.RequestId);
        /// <summary>
        /// 
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}