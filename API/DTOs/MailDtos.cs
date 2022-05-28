namespace Dtos
{
    public record MailToSendDto(string To, string CC, string BCC, string Subject, string Body);
}