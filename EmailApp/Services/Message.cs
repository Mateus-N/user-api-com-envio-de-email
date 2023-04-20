using MimeKit;

namespace EmailApp.Services;

public class Message
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Message(IEnumerable<string> to, string subject, string title, string content)
    {
        To = new List<MailboxAddress>();

        To.AddRange(to.Select(x => new MailboxAddress(subject, x)));
        Subject = subject;
        Title = title;
        Content = content;
    }
}
