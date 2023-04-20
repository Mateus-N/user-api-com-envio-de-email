namespace EmailApp.Dtos;

public class SendEmailDto
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
