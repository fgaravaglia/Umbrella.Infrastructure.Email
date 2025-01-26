using NSubstitute;
using Umbrella.Infrastructure.Email.Core;

namespace Umbrella.Infrastructure.Email.Tests;

public class EmailServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task SendAsync_Success()
    {
        // Arrange
        var mockSender = Substitute.For<IEmailSender>();
        mockSender.SendAsync(Arg.Any<EmailMetadata>()).Returns(Task.FromResult(Guid.NewGuid().ToString()));
        var emailService = new EmailService(mockSender);
        var emailMetadata = new EmailMetadata();

        // Act
        var result = await emailService.SendAsync(emailMetadata);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.RequestId, Is.Not.Null.Or.Empty);
            Assert.That(result.ErrorMessage, Is.Null);
        });
        Assert.Pass();
    }

    [Test]
    public async Task SendAsync_Failure()
    {
        // Arrange
        var mockSender = Substitute.For<IEmailSender>();
        mockSender.SendAsync(Arg.Any<EmailMetadata>()).Returns(Task.FromException<string>(new Exception("Test exception")));

        var emailService = new EmailService(mockSender);
        var emailMetadata = new EmailMetadata(); // Create an instance of EmailMetadata

        // Act
        var result = await emailService.SendAsync(emailMetadata);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.RequestId, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Test exception"));
        });
        Assert.Pass();
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_When_Sender_Is_Null()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new EmailService(null));
        Assert.Pass();
    }
}