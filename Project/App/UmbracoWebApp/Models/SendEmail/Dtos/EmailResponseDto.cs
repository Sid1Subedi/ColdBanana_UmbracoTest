namespace UmbracoWebApp.Models.SendEmail.Dtos
{
    public class EmailResponseDto
    {
        public string To { get; set; }
        public string CC { get; set; } = string.Empty;
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
