using System.Diagnostics.CodeAnalysis;

namespace Umbrella.Infrastructure.Email.Core;

/// <summary>
/// this plain object lets you to model the specific email to send
/// </summary>
[ExcludeFromCodeCoverage]
public class EmailMetadata
{
    /// <summary>
    /// 
    /// </summary>
    public EmailContact From { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<EmailContact> Recipients { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string HtmlBody { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public EmailMetadata()
    {
        this.From = new EmailContact();
        this.Recipients = new List<EmailContact>();
        this.Subject = "";
        this.HtmlBody = "";
    }
}

[ExcludeFromCodeCoverage]
public class EmailContact
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public EmailContact()
    {
        this.Name = "";
        this.Address = "";
    }
}