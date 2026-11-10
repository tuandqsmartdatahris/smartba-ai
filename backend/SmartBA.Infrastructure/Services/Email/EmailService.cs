using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
namespace SmartBA.Infrastructure.Services.Email;
using SmartBA.Application.Interfaces.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOtpAsync(
        string email,
        string otp)
    {
        var tenantId =
            _configuration["MicrosoftGraph:TenantId"];

        var clientId =
            _configuration["MicrosoftGraph:ClientId"];

        var clientSecret =
            _configuration["MicrosoftGraph:ClientSecret"];

        var sender =
            _configuration["MicrosoftGraph:SenderEmail"];

        var credential =
            new ClientSecretCredential(
                tenantId,
                clientId,
                clientSecret);

        var graphClient =
            new GraphServiceClient(
                credential,
                new[] { "https://graph.microsoft.com/.default" }
            );

        var message = new Message
        {
            Subject = "SmartBA AI Verification Code",

            Body = new ItemBody
            {
                ContentType = BodyType.Text,
                Content =
                            $"""
                            Your verification code is:

                            {otp}

                            This code expires in 5 minutes.

                            Regards,
                            SmartBA AI
                            """
            },

            ToRecipients =
            [
                new Recipient
                {
                    EmailAddress =
                        new EmailAddress
                        {
                            Address = email
                        }
                }
            ]
        };

        await graphClient
            .Users[sender]
            .SendMail
            .PostAsync(
                new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
                {
                    Message = message,
                    SaveToSentItems = true
                });
    }
}