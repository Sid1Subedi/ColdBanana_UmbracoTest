using System.ComponentModel.DataAnnotations;

namespace UmbracoWebApp.Models.SendEmail.Dtos
{
    public class EmailRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(2, 50)]
        public string FullName { get; set; }

        [Required]
        [Range (10, 1000)]
        public string Message { get; set; }

        public string Subject { get; set; } = "Let's work together to create something great";
    }
}
