using System.Net.Mail;

namespace Shared.Senders;
public class EmailSender
{
    private readonly SmtpClient smtpClient;
    private readonly MailAddress mailAddress;

    public EmailSender(SmtpClient smtpClient, MailAddress mailAddress)
    {
        this.smtpClient = smtpClient;
        this.mailAddress = mailAddress;
    }

    public async Task SendEmailAsync(string subject, string body, string receiverEmail, string? receiverName = null)
    {
        await SendEmailBulkAsync(subject, body, true, new MailAddress(receiverEmail, receiverName));
    }

    public async Task SendEmailBulkAsync(string subject, string body, bool bcc, params MailAddress[] addresses)
    {
        if (addresses.Length == 0)
            throw new ArgumentException(nameof(addresses));

        MailMessage msg = new(mailAddress, addresses[0]);
        msg.Subject = subject;
        msg.Body = body;

        foreach (var address in addresses.Skip(1))
        {
            if (bcc)
                msg.Bcc.Add(address);
            else
                msg.CC.Add(address);
        }

        await smtpClient.SendMailAsync(msg);
    }
}
