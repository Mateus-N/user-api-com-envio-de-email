using Newtonsoft.Json;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class SendEmailService
{
    private readonly HttpClient httpClient;
    private readonly EmailConnectionUrl connectionUrl;

    public SendEmailService(HttpClient httpClient, EmailConnectionUrl connectionUrl)
    {
        this.httpClient = httpClient;
        this.connectionUrl = connectionUrl;
    }

    public async Task SendToEmailAsync(string email,string subject, string title, string body)
    {
        string json = JsonConvert.SerializeObject(new
        {
            EmailTo = email,
            Subject = subject,
            Title = title,
            Body = body
        });

        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        var httpResponse = await httpClient.PostAsync(connectionUrl.Url, httpContent);
        httpResponse.EnsureSuccessStatusCode();
    }
}
