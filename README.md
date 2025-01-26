# Umbrella.Infrastructure.Email

Repository to store litgh-weight implementation to send email

## How to configure it

First of all, resiter needed depencies using the extensions methods in tha packages. In the example below I used Gmail APi (provided in this repository), but you can implement your own:

```c#
IServiceCollection services = new ServiceCollection();
var service = services.AddUmbrellaEmail()
                        .UsingGmailApi("MY GCP AUTH CLIENT ID", "MY GCP AUTH SECRET ID")
                        .BuildServiceProvider()
                        .GetRequiredService<IEmailService>();
```

then, use the proper IEmailService to send the email:

```c#
    var result = service.SendAsync(new EmailMetadata
    {
        From = new EmailContact
        {
            Name = "sender name",
            Address = "no_reply@support.com"
        },
        Recipients = [new EmailContact
        {
            Name = "target recipient", Address = "recipient@mail.it"
        }],
        Subject = "Test Email",
        HtmlBody = "<b>prova</b> just in case for Html"
    }).GetAwaiter().GetResult();
    Console.WriteLine($"Result: {result.IsSuccess} {result.RequestId} [{result.ErrorMessage}]");
```

## Gmail Api Provider

Here some notes related to Gmail Api:
The name and address provided are ignored. GMAIL Api does not lets you (as far I checked in the official documentation) to change the name and address of sender. So, the recipient will see that the email has been sent from your account (at least, the account who created GCP project where you configured the OAuth2 credentials)
