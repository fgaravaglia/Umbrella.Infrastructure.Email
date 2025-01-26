using Umbrella.Infrastructure.Email.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using MimeKit;

namespace Umbrella.Infrastructure.Email.Gmail;

/// <summary>
/// 
/// </summary>
internal class GmailApiSender : IEmailSender
{
    #region Fields
    readonly string _ApplicationName;
    readonly string _ClientId;
    readonly string _SecretId;
    #endregion

    public GmailApiSender(string? applicationName, string clientId, string secretId)
    {
        this._ApplicationName = string.IsNullOrEmpty(applicationName) ? "Email Sender" : applicationName.Trim();
        ArgumentNullException.ThrowIfNullOrEmpty(clientId);
        ArgumentNullException.ThrowIfNullOrEmpty(secretId);
        this._ClientId = clientId;
        this._SecretId = secretId;
    }
    /// <summary>
    /// Sends the message
    /// </summary>
    /// <param name="emailMetadata"></param>
    /// <returns>in as of success, the UUID of the send email. Otherwise the occurred error</returns>
    public Task<string> SendAsync(EmailMetadata emailMetadata)
    {
        // 1. Carica le credenziali OAuth 2.0
        string[] scopes = { GmailService.Scope.GmailSend };
        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "1007091683299-616j8jnnvo3h2u5cn6pt98iphaee560u.apps.googleusercontent.com",
                ClientSecret = "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"
            },
            scopes,
            "user",
            CancellationToken.None).Result;

        // 2. Crea un servizio Gmail
        var service = new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = this._ApplicationName
        });

        // 3. Crea il messaggio email
        var message = new Message();
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(emailMetadata.From.Name, emailMetadata.From.Address));
        emailMetadata.Recipients.ForEach(x => mimeMessage.To.Add(new MailboxAddress(x.Name, x.Address)));
        mimeMessage.Subject = emailMetadata.Subject;
        // mimeMessage.Body = new TextPart("plain")
        // {
        //     Text = "Questo è il corpo del messaggio"
        // };
        // Crea la parte HTML
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = emailMetadata.HtmlBody;
        // Imposta il corpo del messaggio
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        // 4. Converti il messaggio MIME in una stringa base64url
        var encodedMessage = Base64UrlEncode(mimeMessage.ToString());

        // 5. Crea la richiesta di invio
        var messageBody = new Message { Raw = encodedMessage };
        var request = service.Users.Messages.Send(messageBody, "me");

        // 6. Invia l'email
        var response = request.Execute();
        return Task.FromResult(response.Id);
    }

    private static string Base64UrlEncode(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
