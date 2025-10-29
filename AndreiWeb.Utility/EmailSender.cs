using Microsoft.AspNetCore.Identity.UI.Services;

namespace AndreiWeb.Utility;

public class EmailSender: IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {   //email sending logic
        return Task.CompletedTask;
    }
}