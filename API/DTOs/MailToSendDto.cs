namespace API.Dtos
{
    public class MailToSend
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}