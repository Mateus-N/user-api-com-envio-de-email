using EmailApp.Dtos;
using EmailApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailAppController : ControllerBase
{
    private readonly IEmailSender emailSender;

    public EmailAppController(IEmailSender emailSender)
    {
        this.emailSender = emailSender;
    }

    [HttpPost]
    public async Task SendEmailTo([FromBody] SendEmailDto sendEmailDto)
    {
        var message = new Message(
            new string[] { sendEmailDto.EmailTo },
            sendEmailDto.Subject,
            sendEmailDto.Title,
            sendEmailDto.Body
        );
        await emailSender.SendEmailAsync(message);
    }
}
